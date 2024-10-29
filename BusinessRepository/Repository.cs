using DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ViewModel.VMModel;

namespace BusinessRepository
{
    public class Repository: IRepository
    {

        TechnologyEntities db = new TechnologyEntities();
        Status status = new Status();

        public Status AddSoftware(VMSoftware vmsoftware)
        {
            try
            {
                Software soft = db.Softwares.Where(x => x.SoftwareID == vmsoftware.SoftwareID).FirstOrDefault();


                if(soft == null)
                {
                    soft = new Software();
                    soft.SoftwareID = vmsoftware.SoftwareID;
                    soft.SoftwareName = vmsoftware.SoftwareName;
                    soft.Address = vmsoftware.Address;
                    soft.Rating = vmsoftware.Rating;
                    soft.IsActive = true;
                    soft.CreatedBy = 1;
                    soft.CreatedDate = DateTime.Now;

                    db.Softwares.Add(soft);
                    db.SaveChanges();

                    status.code = System.Net.HttpStatusCode.OK;
                    status.message = " Data Added successfully";
                }
                else
                {
                    soft.SoftwareID = vmsoftware.SoftwareID;
                    soft.SoftwareName = vmsoftware.SoftwareName;
                    soft.Address = vmsoftware.Address;
                    soft.Rating = vmsoftware.Rating;
                    soft.IsActive = true;
                    soft.ModifiedBy = 1;
                    soft.ModifiedDate = DateTime.Now;

                    db.Entry(soft).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    status.code = System.Net.HttpStatusCode.OK;
                    status.message = " Data Updated Successfully";


                }
                return status;

            }
            catch(Exception ex)
            {
                status.message = "Something Went Wrong";
                throw;
            }
          
        }
        

        //======List Start ==============

        public List<VMSoftware> getSoftwareList()
        {
            List<VMSoftware> vmsoftware = new List<VMSoftware>();
            try
            {
                vmsoftware = (from e in db.Softwares
                              where e.IsActive == true
                              select new VMSoftware
                              {
                                  SoftwareID = e.SoftwareID,
                                  SoftwareName = e.SoftwareName,
                                  Address = e.Address,
                                  Rating = e.Rating,
                              }).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
            return vmsoftware;
        }


        // Code for edit Software


        public VMSoftware getsoftwareid(int softwareid)
        {
    
            VMSoftware vmsoftware = new VMSoftware();

            try
            {
                vmsoftware = (from s in db.Softwares
                              where s.SoftwareID == softwareid
                              select new VMSoftware
                              {
                                  SoftwareID = s.SoftwareID,
                                  SoftwareName = s.SoftwareName,
                                  Address = s.Address,
                                  Rating = s.Rating
                              }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                status.code = System.Net.HttpStatusCode.BadGateway;
                throw;
            }
            return vmsoftware;
        }



        //===============Delete=========

        public Status deleteSoftware(int deleteid)
        {
            try
            {
                var delete = db.Softwares.Where(x => x.SoftwareID == deleteid).FirstOrDefault();

                if(delete != null)
                {
                    delete.IsActive = false;
                    db.Entry(delete).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    status.code = HttpStatusCode.OK;
                    status.message = "Data deleted Successfully";
                }
                else
                {
                    status.code = HttpStatusCode.OK;
                    status.message = "Data not deleted";
                }
                
            }
            catch(Exception ex)
            {
                status.code = HttpStatusCode.BadRequest;
                status.isErrorInService = true;
                status.message = " something went wrong , Please try again later...";
                return status;
            }
            return status;
        }
    }
}
