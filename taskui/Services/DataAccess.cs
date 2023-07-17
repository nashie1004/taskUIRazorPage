using taskui.Models;

namespace taskui.Services
{
    public class DataAccess : IDataAccess
    {
        private readonly IDataRepository repository_;

        public DataAccess(IDataRepository repository)
        {
            repository_ = repository;
        }

        public async Task<List<Header>> GetHeaderList()
        {
            var retValue = new List<Header>();
            try
            {
                //retValue = await repository_.GetList<Header>();
                var header = await repository_.GetList<Header>();
                var detail = await repository_.GetList<Detail>();

                foreach (var item in header)
                {
                    item.Detail = detail.Where(p => p.HeaderId == item.HeaderId).ToList();
                }
                return header;
            }
            catch (Exception ex)
            {
            }
            return retValue;
        }

        public async Task SubmitForm(Header submitHeader)
        {
            List<Header> value = new List<Header>();

            try
            {
                await repository_.SubmitAHeader(submitHeader);
            } 
            catch(Exception ex)
            {
                
            }

            return;
        }

        public Header? GetOneReleasePage(int pageId)
        {
            Header? result = null;
            try
            {
                return result = repository_.GetPageInfo(pageId);
            } 
            catch (Exception ex) 
            {
                return result;   
            }
        }

        public void EditAPage(Header modifyThisHeader)
        {
            try
            {
                repository_.EditPage(modifyThisHeader);
            }
            catch (Exception ex) { }
        }
        public void DeleteAPage(int pageId)
        {
            try
            {
                repository_.DeletePage(pageId);
            }
            catch (Exception ex) { }
        }
        public void SubmitTableView(SubmitBody dataJSON)
        {
            try
            {
                repository_.SubmitTableData(dataJSON);
            }
            catch (Exception ex) { }
        }
        public void DeleteDetail(int detailID)
        {
            try
            {
                repository_.DeleteSingleDetail(detailID);
            } catch (Exception ex) { }
        }
        public void AddDetail(int headerId)
        {
            try
            {
                repository_.AddSingleDetail(headerId);
            }
            catch (Exception ex) { }
        }
    }

    public interface IDataAccess
    {
        Task<List<Header>> GetHeaderList();
        Task SubmitForm(Header submitHeader);
        Header? GetOneReleasePage(int id);
        void EditAPage(Header modifyThisHeader);
        void DeleteAPage(int pageId);
        void SubmitTableView(SubmitBody dataJSON);
        void DeleteDetail(int detailID);
        void AddDetail(int headerId);
    }
}