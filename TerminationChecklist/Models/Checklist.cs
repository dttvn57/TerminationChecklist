using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace TerminationChecklist.Models
{
    [Table("HR_TerminationChecklist")]
    public class Checklist
    {
        //[Key]
        //[DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Display(Name = "Employee Name")]
        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Date Personnel Notified")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? date_personnel_notified { get; set; }

        [Required]
        [Display(Name = "Location")]
        public string location { get; set; }

        [Required]
        [Display(Name = "Last Work Day")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? last_work_day { get; set; }

        [Required]
        [Display(Name = "Classification")]
        public string classification { get; set; }

        [Display(Name = "Delete Security Pin Code")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? delete_pin_day { get; set; }

        [Display(Name = "Initials")]
        public string initials { get; set; }

        [Display(Name = "Building Key")]
        public string specify_building_key { get; set; }

        [Display(Name = "Locker #")]
        public string locker_num { get; set; }

        public bool notify_phone_coordinator { get; set; }
        public bool notify_network_coordinator { get; set; }
        public bool remove_user_folder { get; set; }
        public bool remove_infopac_password { get; set; }
        public bool notify_hr { get; set; }
        public bool secretarial_unit { get; set; }
        public bool remove_staff_status { get; set; }

        public string collect_bldg_keys { get; set; }

        public bool special_keys { get; set; }
        public bool health_plan_binder_returned { get; set; }
        public bool name_badge { get; set; }
        public bool county_gas_card { get; set; }
        public bool conflict_of_interest_code_report { get; set; }
        public bool returned_to_itu { get; set; }
        public bool returned_to_accounting_manager { get; set; }

        public string special_equipment { get; set; }

        public string supervisor_signature { get; set; }

        [DataType(DataType.Date)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? supervisor_signed_date { get; set; }

        [DataType(DataType.Date)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? created_date { get; set; }
        public string created_by_lname { get; set; }
        public string created_by_fname { get; set; }
    }
}