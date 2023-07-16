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
            // TODO
            try
            {
                //repository_.EditPage();
            }
            catch (Exception ex) { }
        }
        public void DeleteAPage(int pageId)
        {
            // TODO
            try
            {
                repository_.DeletePage(1);
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
    }
}