using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using taskui.Contexts;
using taskui.Models;
using System.Linq;

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
            if (result != null)
            {
                var allDetails = context_.Detail.ToList();
                var foundItemDetail = allDetails.Where(item => item.HeaderId == pageId).ToList();

                result.Detail = foundItemDetail;
            }
            return result;
        }
        public void EditPage(Header modifyThisHeader)
        {
            try
            {
                // fix this - use uuid to find element
                var found = context_.Header
                    .FirstOrDefault(item => item.ReleaseName == modifyThisHeader.ReleaseName);
                
                if (found != null)
                {
                    found.ReleaseName = modifyThisHeader.ReleaseName;
                    found.ReleaseDate = modifyThisHeader.ReleaseDate;
                    found.ShortDescription = modifyThisHeader.ShortDescription;
                    found.LongDescription = modifyThisHeader.LongDescription;
                    //found.Detail = modifyThisHeader.Detail;
                    
                    var allDetails = context_.Detail.ToList();
                    foreach (var toAddDetail in modifyThisHeader.Detail)
                    {
                        if (allDetails.Any(i => i.DetailId == toAddDetail.DetailId))
                        {
                            allDetails.Remove(toAddDetail);
                            allDetails.Add(toAddDetail);
                        } else
                        {
                            allDetails.Add(toAddDetail);
                        }
                    }
                     

                    context_.SaveChanges();
                }
                

            } catch (Exception ex) { }
        }
        public void DeletePage(int pageId)
        {
            try
            {
                
                var found = context_.Header
                    .FirstOrDefault(item => item.HeaderId == pageId);
                
                if (found != null)
                {
                    context_.Remove(found);
                    context_.SaveChanges();
                }
                
                
            }
            catch (Exception ex) { }
        }
        public void SubmitTableData(SubmitBody data)
        {
            try
            {
                var allHeader = context_.Header.ToList();
                var allDetail = context_.Detail.ToList();

                foreach (var header in data.BodyData)
                {
                    var foundHeader = allHeader.FirstOrDefault(i => i.HeaderId == header.HeaderId);
                    var foundDetail = allDetail.FirstOrDefault(i => i.DetailId == header.DetailId);

                    if (foundHeader != null)
                    {
                        foundHeader.ReleaseName = header.ReleaseNameTable;
                        foundHeader.ReleaseDate = header.ReleaseDateTable;
                        foundHeader.ShortDescription = header.ShortDescription;
                        foundHeader.LongDescription = header.LongDescription;
                    } 
                    if (foundDetail != null)
                    {
                        foundDetail.Type = header.DetailTypeTable;
                        foundDetail.Name = header.DetailNameTable;
                        foundDetail.Description = header.DetailDescriptionTable;
                    }

                    context_.SaveChanges();
                }
            }
            catch (Exception ex) { }
        }
        public void DeleteSingleDetail(int detailID)
        {
            var found = context_.Detail.FirstOrDefault(i => i.DetailId == detailID);

            if (found != null)
            {
                context_.Detail.Remove(found);
                context_.SaveChanges();
            }
        }
        public void AddSingleDetail(int headerId)
        {
            Detail newDetail = new Detail()
            {
                HeaderId = headerId,
                Type = 1,
                Name = "Name here...",
                Description = "Description here..."
            };
            context_.Detail.Add(newDetail);
            context_.SaveChanges();
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
        void SubmitTableData(SubmitBody data);
        void DeleteSingleDetail(int detailID);
        void AddSingleDetail(int headerId);
    }
}
