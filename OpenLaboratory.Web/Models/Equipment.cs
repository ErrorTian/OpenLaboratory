using System.ComponentModel.DataAnnotations;
using OpenLaboratory.Web.Controllers;

namespace OpenLaboratory.Web.Models
{
    public class Equipment
    {
        //设备序号
        public int Id { get; set; }
        //设备状态
        [Display(Name = "状态")]
        public EquipmentStatus Statu { get; set; }
        //设备名称
        [Display(Name = "名称")]
        public string Name { get; set; }

        //设备所属实验室的主键
        [Display(Name = "实验室")]
        public int LaboratoryId { get; set; }
        //导航属性，设备所属实验室
        public Laboratory Laboratory { get; set; }
    }
    public enum EquipmentStatus
    {
        可预约 = 0,
        被占用 = 1,
        维护中 = 2
    }

}
