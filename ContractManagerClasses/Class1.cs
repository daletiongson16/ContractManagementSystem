using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Contract_Management
{
    public class Contract
    {
        public string company_name;
        public int contract_id;
        public bool active;
        public string contract_name;

        public Contract(string company_name, int contract_id, bool active, string contract_name)
        {
            this.company_name = company_name;
            this.contract_id = contract_id;
            this.active = active;
            this.contract_name = contract_name;
        }
    }

    public class ContractVersion
    {
        public int contract_id;
        public DateTime date_revised;
        public string path_to_file;
        public int version_id;

        public ContractVersion(int contract_id, DateTime date_revised, string path_to_file, int version_id)
        {
            this.contract_id = contract_id;
            this.date_revised = date_revised;
            this.path_to_file = path_to_file;
            this.version_id = version_id;
        }
    }

    public class Tag
    {
        public int tag_id;
        public int version_id;
        public string tag_name;
        public int x_coord;
        public int y_coord;

        public Tag(int tag_id, int version_id, string tag_name, int x_coord, int y_coord)
        {
            this.tag_id = tag_id;
            this.version_id = version_id;
            this.tag_name = tag_name;
            this.x_coord = x_coord;
            this.y_coord = y_coord;
        }
    }

    public class TagType
    {
        public string name;
        public string value_type;
        public string description;
        public TagType(string name, string value_type, string description)
        {
            this.name = name;
            this.value_type = value_type;
            this.description = description;
        }
    }

    public class ListEntry
    {
        public string counterparty;
        public string contract;
        public string version;
        public int version_id;

        public ListEntry(string counterparty, string contract, string version, int version_id)
        {
            this.counterparty = counterparty;
            this.contract = contract;
            this.version = version;
            this.version_id = version_id;
        }
    }
}