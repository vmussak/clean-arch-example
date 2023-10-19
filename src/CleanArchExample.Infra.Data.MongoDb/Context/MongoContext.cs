using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchExample.Infra.Data.MongoDb.Context
{
    public class MongoContext : IMongoContext
    {
        private IMongoDatabase Database { get; set; }
        public IClientSessionHandle Session { get; set; }
        public IMongoClient MongoClient { get; set; }
        private readonly List<Func<Task>> _commands;

        public MongoContext(IMongoClient mongoClient, string databaseName)
        {
            MongoClient = mongoClient;
            Database = MongoClient.GetDatabase(databaseName);
            _commands = new List<Func<Task>>();
        }

        public async Task<int> SaveChanges()
        {
            if (!_commands.Any())
                return 0;

            using (Session = await MongoClient.StartSessionAsync().ConfigureAwait(false))
            {
                Session.StartTransaction();

                var commandTasks = _commands.Select(c => c());

                await Task.WhenAll(commandTasks).ConfigureAwait(false);

                await Session.CommitTransactionAsync().ConfigureAwait(false);
            }

            var commandsExecuted = _commands.Count;

            _commands.Clear();

            return commandsExecuted;
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return Database.GetCollection<T>(name);
        }

        public void AddCommand(Func<Task> func)
        {
            _commands.Add(func);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Session?.Dispose();
            }
        }
    }
}
