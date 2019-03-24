using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OpenLaboratory.Web.Models
{
    public class Laboratory
    {
        public Laboratory()
        {
            Equipments = new List<Equipment>();
        }
        //实验室序号
        public int Id { get; set; }
        //实验室名称
        [Display(Name = "名称")]
        public string Name { get; set; }
        //实验室地址
        [Display(Name = "地址")]
        public string Address { get; set; }
        //实验室状态
        [Display(Name = "状态")]
        public LaboratoryStatus Statu { get; set; }

        //一个实验室对应多个设备
        public List<Equipment> Equipments { get; set; }
        
    }

    public enum LaboratoryStatus
    {
        关闭中 = 0,
        值守中 = 1,
    }
}