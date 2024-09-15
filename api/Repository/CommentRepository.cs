using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext _db;
        public CommentRepository(AppDbContext db)
        {
            _db = db;
        }
        public async Task<List<Comment>> GetAllAsync()
        {
            return await _db.Comments.ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _db.Comments.FindAsync(id);
        }
    }
}