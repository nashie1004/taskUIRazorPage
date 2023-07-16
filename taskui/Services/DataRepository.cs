﻿using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using taskui.Contexts;
using taskui.Models;

namespace taskui.Services
{
    public class DataRepository : IDataRepository
    {
        private readonly ReleaseNoteContext context_;

        public ReleaseNoteContext DbContext
        {
            get { return context_; }
        }

        public DataRepository(ReleaseNoteContext context)
        {
            context_ = context;
        }

        public async Task<List<TT>> GetList<TT>()
           where TT : class, new()
        {
            return await context_.Set<TT>().AsNoTracking().ToListAsync();
        }

        public async Task<List<TT>> GetList<TT>(Expression<Func<TT, bool>> predicate)
           where TT : class, new()
        {
            return await context_.Set<TT>()
                .Where(predicate).AsNoTracking().ToListAsync();
        }

        public async Task<TT> GetDetail<TT>(Expression<Func<TT, bool>> predicate)
          where TT : class, new()
        {
            var result = await context_.Set<TT>().AsNoTracking()
                .FirstOrDefaultAsync(predicate);
            return result ?? new TT();
        }

        public async Task<TT> GetDetail<TT>()
           where TT : class, new()
        {
            var result = await context_.Set<TT>().AsNoTracking()
                .FirstOrDefaultAsync();
            return result ?? new TT();
        }

        public async Task<TT> UpdateRecord<TT>(TT model) where TT : class, new()
        {
            context_.Update(model);
            return model;
        }

        public async Task<int> SaveRecord()
        {
            var retValue = await context_.SaveChangesAsync();
            return retValue;
        }

        public async Task<bool> DeleteDetail<TT>(Expression<Func<TT, bool>> predicate) where TT : class, new()
        {
            int result = 0;
            var toRemove = await context_.Set<TT>().FirstOrDefaultAsync(predicate);
            if (toRemove != null)
            {
                context_.Set<TT>().Remove(toRemove);
                result = await context_.SaveChangesAsync();
            }
            return Convert.ToBoolean(result);
        }

        // 4 newly created method
        public async Task SubmitAHeader(Header submitHeader)
        {
            try
            {
                var result = await context_.Header.AddAsync(submitHeader);
                await SaveRecord();
            } 
            catch (Exception ex) 
            { }
            return;
        }
        public Header? GetPageInfo(int pageId)
        {
            Header? result = context_.Header.FirstOrDefault(w => w.HeaderId == pageId);
            return result;
        }
        public void EditPage(Header modifyThisHeader)
        {
            try
            {
                /*
                var found = context_.Header
                    .FirstOrDefault(item => item.HeaderId == modifyThisHeader.HeaderId);
                
                if (found != null)
                {
                    found.ReleaseName = modifyThisHeader.ReleaseName;
                    found.ReleaseDate = modifyThisHeader.ReleaseDate;
                    found.ShortDescription = modifyThisHeader.ShortDescription;
                    found.LongDescription = modifyThisHeader.LongDescription;
                    found.Detail = modifyThisHeader.Detail;

                    context_.SaveChanges();
                }
                */

            } catch (Exception ex) { }
        }
        public void DeletePage(int pageId)
        {
            try
            {
                /*
                var found = context_.Header
                    .FirstOrDefault(item => item.HeaderId == pageId);
                
                if (found != null)
                {
                    context_.Remove(found);
                    context_.SaveChanges();
                }
                */
                
            }
            catch (Exception ex) { }
        }
    }
    public interface IDataRepository
    {
        ReleaseNoteContext DbContext { get; }

        Task<bool> DeleteDetail<TT>(Expression<Func<TT, bool>> predicate) where TT : class, new();

        Task<TT> GetDetail<TT>() where TT : class, new();

        Task<TT> GetDetail<TT>(Expression<Func<TT, bool>> predicate) where TT : class, new();

        Task<List<TT>> GetList<TT>() where TT : class, new();

        Task<List<TT>> GetList<TT>(Expression<Func<TT, bool>> predicate) where TT : class, new();

        Task<int> SaveRecord();

        Task<TT> UpdateRecord<TT>(TT model) where TT : class, new();

        Task SubmitAHeader(Header submitHeader);
        Header? GetPageInfo(int pageId);
        void EditPage(Header modifyThisHeader);
        void DeletePage(int pageId);
    }
}
