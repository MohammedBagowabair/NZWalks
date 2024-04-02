﻿using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLWalkRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Walk> CreateAsync(Walk walk)
        {
            await dbContext.AddAsync(walk);
            await dbContext.SaveChangesAsync();

            return walk;
        }

       public async Task<List<Walk>> GetAllAsync()
        {
           return await dbContext.Walks.Include("Region").Include("Difficulty").ToListAsync();
           
        }

       public async Task<Walk?> GetByIdAsync(Guid id)
        {
            return await dbContext.Walks.Include("Region").Include("Difficulty").FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            var existingWalk=await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingWalk == null) 
            {
                return null;
            }

            existingWalk.Name = walk.Name;
            existingWalk.Description = walk.Description;
            existingWalk.WalkImageUrl = walk.WalkImageUrl;
            existingWalk.LengthInKm = walk.LengthInKm;
            existingWalk.RegionId= walk.RegionId;
            existingWalk.DifficultyId = walk.DifficultyId;

            await dbContext.SaveChangesAsync();

            return existingWalk;

        }

       public async Task<Walk?> DeleteAsync(Guid id)
        {
            var existingWalk = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingWalk == null)
            {
                return null;
            }

            dbContext.Walks.Remove(existingWalk);
            await dbContext.SaveChangesAsync();

            return existingWalk;
        }
    }
}