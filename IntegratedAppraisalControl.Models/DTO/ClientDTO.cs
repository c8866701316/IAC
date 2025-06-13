using System;
using System.Collections.Generic;
using System.Text;

namespace IntegratedAppraisalControl.Models.DTO
{
    public class ClientDto
    {
        public PaginatedList<TblClientsDTO> data { get; set; }

        public ClientSearchCriteriaModel searchCriteria { get; set; }
    }
}
