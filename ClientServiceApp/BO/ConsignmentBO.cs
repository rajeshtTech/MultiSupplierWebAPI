using ClientServiceApp.Infrastructure.Helper;
using ClientServiceApp.Models;
using ClientServiceApp.Repositories.WebAPIRepositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        public ConsignmentBO(ISuppXConsignRepository repoX,
                             ISuppYConsignRepository repoY,
                             ISuppZConsignRepository repoZ,
                             IUtility utility) 
        {
            _repoX = repoX;
            _repoY = repoY;
            _repoZ = repoZ;
            _utility = utility;
        }

        public async Task<SupplierQuoteResponse> GetLowestConsignmentQuote(ConsginmentDetails consignDetails)
        {
            // GET QUOTE FOR SUPPLIER X
            var suppQuote = await _repoX.GetConsignmentQuote(_utility.GetAutoMappedObject<ConsginmentDetails, XConsignDetailsModel>(consignDetails));
            if(SupplierQuoteResponseSuccess(suppQuote) == false) 
                return suppQuote; 

            // GET QUOTE FOR SUPPLIER Y
            suppQuote = await _repoY.GetConsignmentQuote(_utility.GetAutoMappedObject<ConsginmentDetails, YConsignDetailsModel>(consignDetails));
            if (SupplierQuoteResponseSuccess(suppQuote) == false)
                return suppQuote;

            // GET QUOTE FOR SUPPLIER Z
            suppQuote = await _repoZ.GetConsignmentQuote(_utility.GetAutoMappedObject<ConsginmentDetails, ZConsignDetailsModel>(consignDetails));
            if (SupplierQuoteResponseSuccess(suppQuote) == false)
                return suppQuote;

            return suppQuoteList.Count() != 0 ? suppQuoteList.OrderBy(supp => supp.QuoteAmount).FirstOrDefault()
                                                :new SupplierQuoteResponse { ResponseStatusCode = HttpStatusCode.NoContent, ErrorResponse = "Non Deliverable Location(s)"};
        }

        #region PRIVATE METHODS
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
