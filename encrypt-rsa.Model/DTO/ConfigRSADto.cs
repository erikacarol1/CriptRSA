using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace encrypt_rsa.Model.DTO
{
    public class ConfigRSADto
    {
        public ConfigRSADto()
        {
            this.p = 17;
            this.q = 19;
            this.n =this.p *this.q;
        }
        [Required]
        public string message { get; set; }
        public virtual int p { get; set; }
        public virtual int q { get; set; }
        public virtual int d { get; set; }
        public virtual int n { get; set; }
        public virtual int e { get; set; }
        public virtual int totiente { get; set; }
    }
}
