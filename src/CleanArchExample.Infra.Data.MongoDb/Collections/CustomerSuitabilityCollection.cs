using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace CleanArchExample.Infra.Data.MongoDb.Collections
{
    public class CustomerSuitabilityCollection
    {
        [BsonId]
        [BsonElement]
        public Guid Id { get; set; }

        [BsonElement]
        [BsonRepresentation(BsonType.Int64)]
        public long Cpf { get; set; }

        [BsonElement]
        public List<InvestorProfileQuestionCollection> AnsweredQuestions { get; set; }

        [BsonElement]
        [BsonRepresentation(BsonType.Int32)]
        public int InvestorProfile { get; set; }

        [BsonElement]
        [BsonRepresentation(BsonType.Int32)]
        public int TotalValue { get; set; }

        [BsonElement]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime CreatedAt { get; set; }
    }

    public class InvestorProfileQuestionCollection
    {
        [BsonElement]
        [BsonRepresentation(BsonType.String)]
        public string Description { get; set; }

        [BsonElement]
        public List<InvestorProfileAnswerCollection> Answers { get; set; }
    }

    public class InvestorProfileAnswerCollection
    {
        [BsonElement]
        [BsonRepresentation(BsonType.String)]
        public string Description { get; set; }

        [BsonElement]
        [BsonRepresentation(BsonType.Int32)]
        public int Value { get; set; }
    }
}
