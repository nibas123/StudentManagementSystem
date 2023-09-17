using MongoDB.Bson;
using MongoDB.Driver;
using Studentms.Models;

namespace Studentms.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly IMongoCollection<Student> _Student;

        public StudentRepository(IMongoClient client)
        {
            var database = client.GetDatabase("StudentDB");
            var collection = database.GetCollection<Student>(nameof(Student));

            _Student = collection;
        }

        public async Task<ObjectId> Create(Student Student)
        {
            await _Student.InsertOneAsync(Student);

            return Student.Id;
        }

        public Task<Student> Get(ObjectId objectId)
        {
            var filter = Builders<Student>.Filter.Eq(c => c.Id, objectId);
            var Student = _Student.Find(filter).FirstOrDefaultAsync();

            return Student;
        }

        public async Task<IEnumerable<Student>> Get()
        {
            var Students = await _Student.Find(_ => true).ToListAsync();

            return Students;
        }

        public async Task<IEnumerable<Student>> GetByName(string Name)
        {
            var filter = Builders<Student>.Filter.Eq(c => c.Name, Name);
            var Students = await _Student.Find(filter).ToListAsync();

            return Students;
        }

        public async Task<bool> Update(ObjectId objectId, Student Student)
        {
            var filter = Builders<Student>.Filter.Eq(c => c.Id, objectId);
            var update = Builders<Student>.Update
                .Set(c => c.Name, Student.Name)
                .Set(c => c.Phone, Student.Phone)
                .Set(c => c.Address, Student.Address);
            var result = await _Student.UpdateOneAsync(filter, update);

            return result.ModifiedCount == 1;
        }

        public async Task<bool> Delete(ObjectId objectId)
        {
            var filter = Builders<Student>.Filter.Eq(c => c.Id, objectId);
            var result = await _Student.DeleteOneAsync(filter);

            return result.DeletedCount == 1;
        }

       
    }
}
