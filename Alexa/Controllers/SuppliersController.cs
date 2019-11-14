using Alexa.Service;
using Alexa.Service.Dtos;
using Alexa.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace Alexa.Controllers
{
    public class SuppliersController : Controller
    {
        private ISupplierService SupplierService;
        private IGroupService GroupService;

        public SuppliersController()
        {
            SupplierService = new SupplierService();
            GroupService = new GroupService();
        }

        public SuppliersController(ISupplierService ss, IGroupService gs)
        {
            SupplierService = ss;
            GroupService = gs;
        }

        // GET: Suppliers
        public ActionResult Index()
        {
            var suppliers = SupplierService.GetAll();

            return View(suppliers);
        }

        // GET: Suppliers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SupplierDto supplier = SupplierService.GetById(id);

            if (supplier == null)
            {
                return HttpNotFound();
            }

            return View(supplier);
        }

        // GET: Suppliers/Create
        public ActionResult Create()
        {
            SupplierDto supplier = SupplierService.Create();
            supplier.AllGroups = GroupService.getAllGroups();

            return View(supplier);
        }

        // POST: Suppliers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Address,Email,Phone,SelectedItems")] SupplierDto supplier)
        {
            if (ModelState.IsValid)
            {
                if (supplier.SelectedItems != null
                    && supplier.SelectedItems.Length > 0)
                {
                    supplier.Groups = new List<GroupDto>();

                    /* Split SelectedGroups by ';' */
                    string[] groupStrs = supplier.SelectedItems.Split(';');

                    /* Iterate through groupStrs, add selected groups into supplierDb and remeber tested groups */
                    foreach (string groupStr in groupStrs)
                    {
                        try
                        {
                            int id = Int32.Parse(groupStr.Replace("Group_", ""));
                            supplier.Groups.Add(GroupService.GetById(id));
                        }
                        catch (FormatException)
                        {
                            ;
                        }
                    }
                }

                SupplierService.Add(supplier);

                return RedirectToAction("Index");
            }

            return View(supplier);
        }

        // GET: Suppliers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SupplierDto supplier = SupplierService.GetById(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }

            supplier.AllGroups = GroupService.getAllGroups();

            return View(supplier);
        }

        // POST: Suppliers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Address,Email,Phone,SelectedItems")] SupplierDto supplier)
        {
            if (ModelState.IsValid)
            {
                /* Load Supplier from DB to have actual List of Groups */
                SupplierDto supplierDb = SupplierService.GetById(supplier.ID);

                supplierDb.Name = supplier.Name;
                supplierDb.Address = supplier.Address;
                supplierDb.Email = supplier.Email;
                supplierDb.Phone = supplier.Phone;

                var checkedGroups = new List<GroupDto>();

                if (supplier.SelectedItems != null
                    && supplier.SelectedItems.Length > 0)
                {
                    /* Split SelectedGroups by ';' */
                    string[] groupStrs = supplier.SelectedItems.Split(';');

                    /* Iterate through groupStrs, add selected groups into supplierDb and remeber tested groups */
                    foreach (string groupStr in groupStrs)
                    {
                        try
                        {
                            int id = Int32.Parse(groupStr.Replace("Group_", ""));
                            GroupDto group = GroupService.GetById(id);

                            /* Mark group as tested */
                            checkedGroups.Add(group);

                            if (supplierDb.Groups.Contains(group) == false)
                            {
                                supplierDb.Groups.Add(group);
                            }
                        }
                        catch (FormatException)
                        {
                            ;
                        }
                    }
                }

                /* Select groups for removing */
                var groupsToRemove = new List<GroupDto>();
                foreach (GroupDto group in supplierDb.Groups)
                {
                    if (checkedGroups.Contains(group) == false)
                    {
                        groupsToRemove.Add(group);
                    }
                }

                /* Remove selected groups */
                foreach (var group in groupsToRemove)
                {
                    supplierDb.Groups.Remove(group);
                }

                /* Save changes into DB */
                SupplierService.Edit(supplierDb);

                return RedirectToAction("Index");
            }
            return View(supplier);
        }

        // GET: Suppliers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SupplierDto supplier = SupplierService.GetById(id);

            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // POST: Suppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SupplierService.Delete(id);

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (SupplierService is IDisposable)
            {
                ((IDisposable)SupplierService).Dispose();
            }

            if (GroupService is IDisposable)
            {
                ((IDisposable)GroupService).Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
