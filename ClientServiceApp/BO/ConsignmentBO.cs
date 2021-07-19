using ClientServiceApp.Infrastructure.Configuration;
using ClientServiceApp.Infrastructure.Helper;
using ClientServiceApp.Models;
using ClientServiceApp.Repositories.WebAPIRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace ClientServiceApp.BO
{
    public class ConsignmentBO : IConsignmentBO
    {
        ISuppXConsignRepository _repoX;
        ISuppYConsignRepository _repoY;
        ISuppZConsignRepository _repoZ;
        IUtility _utility;
        private List<SupplierQuoteResponse> suppQuoteList = new List<SupplierQuoteResponse>();
        AppSettings _appSettings;
        public ConsignmentBO(ISuppXConsignRepository repoX,
                             ISuppYConsignRepository repoY,
                             ISuppZConsignRepository repoZ,
                             IUtility utility,
                             IOptions<AppSettings> appSettings) 
        {
            _repoX = repoX;
            _repoY = repoY;
            _repoZ = repoZ;
            _utility = utility;
            _appSettings = appSettings.Value;
        }

        public async Task<SupplierQuoteResponse> GetLowestConsignmentQuote(ConsginmentDetails consignDetails)
        {            
            foreach (var supplier in _appSettings.SuppliersAPIList)
            {
                Type suplierConsFormat = GetSupplierConsignmentFormat(supplier.Name);

                MethodInfo method = _utility.GetType().GetMethod(Constants.GET_AUTO_MAPPED_OBJECT).MakeGenericMethod(new Type[] { typeof(ConsginmentDetails), suplierConsFormat });
                var suppConsDetails =  method.Invoke(_utility, new object[] { consignDetails }) as BaseConsignDetailsModel;

                var suppQuote = await GetSupplierRepoRefrence(supplier.Name).GetConsignmentQuote(suppConsDetails);
                if (SupplierQuoteResponseSuccess(suppQuote) == false)
                    return suppQuote;
            }

            return suppQuoteList.Count() != 0 ? suppQuoteList.OrderBy(supp => supp.QuoteAmount).FirstOrDefault()
                                                :new SupplierQuoteResponse { ResponseStatusCode = HttpStatusCode.NotFound, ErrorResponse = "Non Deliverable Location(s)"};
        }

        #region PRIVATE METHODS
        private Type GetSupplierConsignmentFormat(string supplierName)
        {
            switch (supplierName)
            {
                case Constants.SUPPLIER_X: return typeof(XConsignDetailsModel);
                case Constants.SUPPLIER_Y: return typeof(YConsignDetailsModel);
                case Constants.SUPPLIER_Z: return typeof(ZConsignDetailsModel);
                default: return null;
            }
        }

        private ISuppConsignRepositoryBase GetSupplierRepoRefrence(string supplierName)
        {
            switch (supplierName)
            {
                case Constants.SUPPLIER_X: return _repoX;
                case Constants.SUPPLIER_Y: return _repoY;
                case Constants.SUPPLIER_Z: return _repoZ;
                default: return null;
            }
        }


        private bool SupplierQuoteResponseSuccess(SupplierQuoteResponse suppQuote)
        {
            if (suppQuote.ResponseStatusCode == HttpStatusCode.OK)
                suppQuoteList.Add(suppQuote);               
            else if (suppQuote.ResponseStatusCode != HttpStatusCode.NoContent)
                return false;

            return true;
        }
        #endregion

    }
}
