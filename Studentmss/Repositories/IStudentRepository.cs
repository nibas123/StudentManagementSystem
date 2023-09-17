using Microsoft.AspNetCore.Builder;
using MongoDB.Bson;
using Studentms.Models;

namespace Studentms.Repositories
{
    public interface IStudentRepository
    {
        // Create
        Task<ObjectId> Create(Student Student);

        // Read
        Task<Student> Get(ObjectId objectId);
        Task<IEnumerable<Student>> Get();
        Task<IEnumerable<Student>> GetByName(string Name);

        // Update
        Task<bool> Update(ObjectId objectId, Student Student);

        // Delete
        Task<bool> Delete(ObjectId objectId);
    }

}
