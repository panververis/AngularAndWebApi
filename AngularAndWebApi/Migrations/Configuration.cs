namespace AngularAndWebApi.Migrations
{
    using AngularAndWebApi.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AngularAndWebApi.Models.AngularAndWebApiContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        /// <summary>
        /// Protected overriden method seeding our Code-First generated Database.
        /// In total it holds:
        /// 2 Areas
        /// 2 Regions for each Area
        /// 20 vehicles
        /// 4 dealers (two in each Area)
        /// 16 staff members, 4 for each Dealer (2 Sales Representatives, 1 Accountant and 1 manager per Dealer)
        /// 20 Sales (2 Sales for each Staff member of the "northern" Regions, 1 per Sales Staff member of the "southern" A Region, 
        ///         repeated for "DateTime.Today" and "DateTime.Today + 3 days")
        /// </summary>
        protected override void Seed(AngularAndWebApi.Models.AngularAndWebApiContext context)
        {
            
            #region Entities' variables declaration

            Area northArea, southArea;
            Region northernRegionA, northernRegionB, southernRegionA, southernRegionB;
            Vehicle vehicle1, vehicle2, vehicle3, vehicle4, vehicle5, vehicle6, vehicle7, vehicle8, vehicle9, vehicle10,
                vehicle11, vehicle12, vehicle13, vehicle14, vehicle15, vehicle16, vehicle17, vehicle18, vehicle19, vehicle20;
            Dealer northernADealer, northernBDealer, southernADealer, southernBDealer;
            Staff northernADealerSalesStaff1, northernADealerSalesStaff2, northernADealerSalesStaff3, northernADealerSalesStaff4,
            northernBDealerSalesStaff1, northernBDealerSalesStaff2, northernBDealerSalesStaff3, northernBDealerSalesStaff4, southernADealerSalesStaff1,
            southernADealerSalesStaff2, southernADealerSalesStaff3, southernADealerSalesStaff4, southernBDealerSalesStaff1, southernBDealerSalesStaff2,
            southernBDealerSalesStaff3, southernBDealerSalesStaff4;
            Sale sale1, sale2, sale3, sale4, sale5, sale6, sale7, sale8, sale9, sale10, sale11, sale12, sale13,
            sale14, sale15, sale16, sale17, sale18, sale19, sale20;

            #endregion

            #region Defining the total of the data

            #region Areas handling

            #region Areas Initialization

            northArea = new Area() { Name = "North Area", ID = 1 };
            southArea = new Area() { Name = "South Area", ID = 2 };

            #endregion

            #region Areas "Upsert" operations

            context.Areas.AddOrUpdate(
                    x => x.ID,
                    northArea,
                    southArea
                );

            #endregion

            context.SaveChanges();

            #region Retrieving the posted Areas (along with their updated IDs)

            northArea = context.Areas.FirstOrDefault(x => x.Name == northArea.Name);
            southArea = context.Areas.FirstOrDefault(x => x.Name == southArea.Name);

            #endregion

            #region

            #endregion

            #endregion

            #region Regions handling

            #region Regions Initialization

            //Creating the initial Regions. There will be four initial Regions
            northernRegionA = new Region() { Name = "Northern Region A", ID = 1, AreaID = northArea.ID };
            northernRegionB = new Region() { Name = "Northern Region B", ID = 2, AreaID = northArea.ID };
            southernRegionA = new Region() { Name = "Southern Region A", ID = 3, AreaID = southArea.ID };
            southernRegionB = new Region() { Name = "Southern Region B", ID = 4, AreaID = southArea.ID };

            #endregion

            #region Regions "Upsert" Operations

            context.Regions.AddOrUpdate(
                    x => x.ID,
                    northernRegionA,
                    northernRegionB,
                    southernRegionA,
                    southernRegionB
                );

            #endregion

            //posting the changes to the DB
            context.SaveChanges();

            #region Retrieving the posted Regions (along with their updated IDs)

            northernRegionA = context.Regions.FirstOrDefault(x => x.Name == northernRegionA.Name);
            northernRegionB = context.Regions.FirstOrDefault(x => x.Name == northernRegionB.Name);
            southernRegionA = context.Regions.FirstOrDefault(x => x.Name == southernRegionA.Name);
            southernRegionB = context.Regions.FirstOrDefault(x => x.Name == southernRegionB.Name);

            #endregion

            #endregion

            #region Vehicles handling

            #region Vehicles Initialization

            vehicle1  = new Vehicle()  { Model = "Toyota Avensis", MakeYear = 2014, ChassisNumber = "Chassis_Num_1",  EngineCapacity = 1400, ID = 1  };
            vehicle2  = new Vehicle()  { Model = "Toyota Avensis", MakeYear = 2014, ChassisNumber = "Chassis_Num_2",  EngineCapacity = 1400, ID = 2  };
            vehicle3  = new Vehicle()  { Model = "Toyota Avensis", MakeYear = 2014, ChassisNumber = "Chassis_Num_3",  EngineCapacity = 1400, ID = 3  };
            vehicle4  = new Vehicle()  { Model = "Toyota Avensis", MakeYear = 2015, ChassisNumber = "Chassis_Num_4",  EngineCapacity = 1600, ID = 4  };
            vehicle5  = new Vehicle()  { Model = "Toyota Avensis", MakeYear = 2016, ChassisNumber = "Chassis_Num_5",  EngineCapacity = 1800, ID = 5  };

            vehicle6  = new Vehicle()  { Model = "Peugeot 207",    MakeYear = 2015, ChassisNumber = "Chassis_Num_6",  EngineCapacity = 1300, ID = 6  };
            vehicle7  = new Vehicle()  { Model = "Peugeot 207",    MakeYear = 2015, ChassisNumber = "Chassis_Num_7",  EngineCapacity = 1400, ID = 7  };
            vehicle8  = new Vehicle()  { Model = "Peugeot 207",    MakeYear = 2015, ChassisNumber = "Chassis_Num_8",  EngineCapacity = 1400, ID = 8  };
            vehicle9  = new Vehicle()  { Model = "Peugeot 207",    MakeYear = 2016, ChassisNumber = "Chassis_Num_9",  EngineCapacity = 1600, ID = 9  };
            vehicle10 = new Vehicle()  { Model = "Peugeot 207",    MakeYear = 2017, ChassisNumber = "Chassis_Num_10", EngineCapacity = 1800, ID = 10 };

            vehicle11 = new Vehicle()  { Model = "Scoda Octavia",  MakeYear = 2009, ChassisNumber = "Chassis_Num_11", EngineCapacity = 1600, ID = 11 };
            vehicle12 = new Vehicle()  { Model = "Scoda Octavia",  MakeYear = 2009, ChassisNumber = "Chassis_Num_12", EngineCapacity = 1600, ID = 12 };
            vehicle13 = new Vehicle()  { Model = "Scoda Octavia",  MakeYear = 2009, ChassisNumber = "Chassis_Num_13", EngineCapacity = 1600, ID = 13 };
            vehicle14 = new Vehicle()  { Model = "Scoda Octavia",  MakeYear = 2009, ChassisNumber = "Chassis_Num_14", EngineCapacity = 1800, ID = 14 };
            vehicle15 = new Vehicle()  { Model = "Scoda Octavia",  MakeYear = 2009, ChassisNumber = "Chassis_Num_15", EngineCapacity = 1800, ID = 15 };

            vehicle16 = new Vehicle()  { Model = "Scoda Fabia",    MakeYear = 2010, ChassisNumber = "Chassis_Num_16", EngineCapacity = 1800, ID = 16 };
            vehicle17 = new Vehicle()  { Model = "Scoda Fabia",    MakeYear = 2010, ChassisNumber = "Chassis_Num_17", EngineCapacity = 1800, ID = 17 };
            vehicle18 = new Vehicle()  { Model = "Scoda Fabia",    MakeYear = 2010, ChassisNumber = "Chassis_Num_18", EngineCapacity = 2000, ID = 18 };
            vehicle19 = new Vehicle()  { Model = "Scoda Fabia",    MakeYear = 2012, ChassisNumber = "Chassis_Num_19", EngineCapacity = 2000, ID = 19 };
            vehicle20 = new Vehicle()  { Model = "Scoda Fabia",    MakeYear = 2013, ChassisNumber = "Chassis_Num_20", EngineCapacity = 2000, ID = 20 };

            #endregion

            #region Vehicles "Upsert" Operations

            context.Vehicles.AddOrUpdate(x => x.ID,
                    vehicle1, vehicle2, vehicle3, vehicle4, vehicle5, vehicle6, vehicle7, vehicle8, vehicle9, vehicle10,
                        vehicle11, vehicle12, vehicle13, vehicle14, vehicle15, vehicle16, vehicle17, vehicle18, vehicle19, vehicle20);

            #endregion

            context.SaveChanges();

            #region Retrieving the posted Vehicles (along with their updated IDs)

            vehicle1 =  context.Vehicles.FirstOrDefault(x => x.ChassisNumber ==  vehicle1.ChassisNumber);
            vehicle2 =  context.Vehicles.FirstOrDefault(x => x.ChassisNumber ==  vehicle2.ChassisNumber);
            vehicle3 =  context.Vehicles.FirstOrDefault(x => x.ChassisNumber ==  vehicle3.ChassisNumber);
            vehicle4 =  context.Vehicles.FirstOrDefault(x => x.ChassisNumber ==  vehicle4.ChassisNumber);
            vehicle5 =  context.Vehicles.FirstOrDefault(x => x.ChassisNumber ==  vehicle5.ChassisNumber);
            vehicle6 =  context.Vehicles.FirstOrDefault(x => x.ChassisNumber ==  vehicle6.ChassisNumber);
            vehicle7 =  context.Vehicles.FirstOrDefault(x => x.ChassisNumber ==  vehicle7.ChassisNumber);
            vehicle8 =  context.Vehicles.FirstOrDefault(x => x.ChassisNumber ==  vehicle8.ChassisNumber);
            vehicle9 =  context.Vehicles.FirstOrDefault(x => x.ChassisNumber ==  vehicle9.ChassisNumber);
            vehicle10 = context.Vehicles.FirstOrDefault(x => x.ChassisNumber == vehicle10.ChassisNumber);
            vehicle11 = context.Vehicles.FirstOrDefault(x => x.ChassisNumber == vehicle11.ChassisNumber);
            vehicle12 = context.Vehicles.FirstOrDefault(x => x.ChassisNumber == vehicle12.ChassisNumber);
            vehicle13 = context.Vehicles.FirstOrDefault(x => x.ChassisNumber == vehicle13.ChassisNumber);
            vehicle14 = context.Vehicles.FirstOrDefault(x => x.ChassisNumber == vehicle14.ChassisNumber);
            vehicle15 = context.Vehicles.FirstOrDefault(x => x.ChassisNumber == vehicle15.ChassisNumber);
            vehicle16 = context.Vehicles.FirstOrDefault(x => x.ChassisNumber == vehicle16.ChassisNumber);
            vehicle17 = context.Vehicles.FirstOrDefault(x => x.ChassisNumber == vehicle17.ChassisNumber);
            vehicle18 = context.Vehicles.FirstOrDefault(x => x.ChassisNumber == vehicle18.ChassisNumber);
            vehicle19 = context.Vehicles.FirstOrDefault(x => x.ChassisNumber == vehicle19.ChassisNumber);
            vehicle20 = context.Vehicles.FirstOrDefault(x => x.ChassisNumber == vehicle20.ChassisNumber);

            #endregion

            #endregion

            #region Dealers handling

            #region Dealers Initialization

            northernADealer = new Dealer() { Name = "Northern Dealer A", RegionID = northernRegionA.ID, ID = 1 };
            northernBDealer = new Dealer() { Name = "Northern Dealer B", RegionID = northernRegionB.ID, ID = 2 };
            southernADealer = new Dealer() { Name = "Southern Dealer A", RegionID = southernRegionA.ID, ID = 3 };
            southernBDealer = new Dealer() { Name = "Southern Dealer B", RegionID = southernRegionB.ID, ID = 4 };

            #endregion

            #region Regions "Upsert" Operations

            context.Dealers.AddOrUpdate(x => x.ID,
                    northernADealer, northernBDealer, southernADealer, southernBDealer);

            #endregion

            context.SaveChanges();

            #region Retrieving the posted Dealers (along with their updated IDs)

            northernADealer = context.Dealers.FirstOrDefault(x => x.Name == northernADealer.Name);
            northernBDealer = context.Dealers.FirstOrDefault(x => x.Name == northernBDealer.Name);
            southernADealer = context.Dealers.FirstOrDefault(x => x.Name == southernADealer.Name);
            southernBDealer = context.Dealers.FirstOrDefault(x => x.Name == southernBDealer.Name);

            #endregion

            #endregion

            #region Staff handling

            #region Staff Initialization

            northernADealerSalesStaff1 = new Staff() { DealerID = northernADealer.ID, FirstName = "Joycelyn",   LastName = "Hartsock",      JobType = JobType.SalesRep,     ID = 1 };
            northernADealerSalesStaff2 = new Staff() { DealerID = northernADealer.ID, FirstName = "Domingo",    LastName = "Clem",          JobType = JobType.SalesRep,     ID = 2 };
            northernADealerSalesStaff3 = new Staff() { DealerID = northernADealer.ID, FirstName = "Glen",       LastName = "Bollig",        JobType = JobType.Accounting,   ID = 3 };
            northernADealerSalesStaff4 = new Staff() { DealerID = northernADealer.ID, FirstName = "Tierra",     LastName = "Rosenzweig",    JobType = JobType.Manager,      ID = 4 };
            northernBDealerSalesStaff1 = new Staff() { DealerID = northernBDealer.ID, FirstName = "Sanjuana",   LastName = "Disla",         JobType = JobType.SalesRep,     ID = 5 };
            northernBDealerSalesStaff2 = new Staff() { DealerID = northernBDealer.ID, FirstName = "Leland",     LastName = "Kono",          JobType = JobType.SalesRep,     ID = 6 };
            northernBDealerSalesStaff3 = new Staff() { DealerID = northernBDealer.ID, FirstName = "Freeman",    LastName = "Bagnall",       JobType = JobType.Accounting,   ID = 7 };
            northernBDealerSalesStaff4 = new Staff() { DealerID = northernBDealer.ID, FirstName = "Debbie",     LastName = "Murton",        JobType = JobType.Manager,      ID = 8 };
            southernADealerSalesStaff1 = new Staff() { DealerID = southernADealer.ID, FirstName = "Israel",     LastName = "Janas",         JobType = JobType.SalesRep,     ID = 9 };
            southernADealerSalesStaff2 = new Staff() { DealerID = southernADealer.ID, FirstName = "Karrie",     LastName = "Mccorkle",      JobType = JobType.SalesRep,     ID = 10 };
            southernADealerSalesStaff3 = new Staff() { DealerID = southernADealer.ID, FirstName = "Aliza",      LastName = "Pickell",       JobType = JobType.Accounting,   ID = 11 };
            southernADealerSalesStaff4 = new Staff() { DealerID = southernADealer.ID, FirstName = "Venita",     LastName = "Sobczak",       JobType = JobType.Manager,      ID = 12 };
            southernBDealerSalesStaff1 = new Staff() { DealerID = southernBDealer.ID, FirstName = "Kacy",       LastName = "Nicastro",      JobType = JobType.SalesRep,     ID = 13 };
            southernBDealerSalesStaff2 = new Staff() { DealerID = southernBDealer.ID, FirstName = "Adele",      LastName = "Gessner",       JobType = JobType.SalesRep,     ID = 14 };
            southernBDealerSalesStaff3 = new Staff() { DealerID = southernBDealer.ID, FirstName = "Tameka",     LastName = "Pawlowicz",     JobType = JobType.Accounting,   ID = 15 };
            southernBDealerSalesStaff4 = new Staff() { DealerID = southernBDealer.ID, FirstName = "Giuseppe",   LastName = "Beckmann",      JobType = JobType.Manager,      ID = 16 };

            #endregion

            #region Staff "Upsert" Operations

            context.Staffs.AddOrUpdate(x => x.ID,
                    northernADealerSalesStaff1, northernADealerSalesStaff2, northernADealerSalesStaff3, northernADealerSalesStaff4, 
                    northernBDealerSalesStaff1, northernBDealerSalesStaff2, northernBDealerSalesStaff3, northernBDealerSalesStaff4,
                    southernADealerSalesStaff1, southernADealerSalesStaff2, southernADealerSalesStaff3, southernADealerSalesStaff4,
                    southernBDealerSalesStaff1, southernBDealerSalesStaff2, southernBDealerSalesStaff3, southernBDealerSalesStaff4);

            #endregion

            context.SaveChanges();

            #region Retrieving the posted Staff (along with their updated IDs)

            northernADealerSalesStaff1 = context.Staffs.FirstOrDefault(x => x.FirstName == northernADealerSalesStaff1.FirstName && x.LastName == northernADealerSalesStaff1.LastName);
            northernADealerSalesStaff2 = context.Staffs.FirstOrDefault(x => x.FirstName == northernADealerSalesStaff2.FirstName && x.LastName == northernADealerSalesStaff2.LastName);
            northernADealerSalesStaff3 = context.Staffs.FirstOrDefault(x => x.FirstName == northernADealerSalesStaff3.FirstName && x.LastName == northernADealerSalesStaff3.LastName);
            northernADealerSalesStaff4 = context.Staffs.FirstOrDefault(x => x.FirstName == northernADealerSalesStaff4.FirstName && x.LastName == northernADealerSalesStaff4.LastName);
            northernBDealerSalesStaff1 = context.Staffs.FirstOrDefault(x => x.FirstName == northernBDealerSalesStaff1.FirstName && x.LastName == northernBDealerSalesStaff1.LastName);
            northernBDealerSalesStaff2 = context.Staffs.FirstOrDefault(x => x.FirstName == northernBDealerSalesStaff2.FirstName && x.LastName == northernBDealerSalesStaff2.LastName);
            northernBDealerSalesStaff3 = context.Staffs.FirstOrDefault(x => x.FirstName == northernBDealerSalesStaff3.FirstName && x.LastName == northernBDealerSalesStaff3.LastName);
            southernADealerSalesStaff1 = context.Staffs.FirstOrDefault(x => x.FirstName == southernADealerSalesStaff1.FirstName && x.LastName == southernADealerSalesStaff1.LastName);
            southernADealerSalesStaff2 = context.Staffs.FirstOrDefault(x => x.FirstName == southernADealerSalesStaff2.FirstName && x.LastName == southernADealerSalesStaff2.LastName);
            southernADealerSalesStaff3 = context.Staffs.FirstOrDefault(x => x.FirstName == southernADealerSalesStaff3.FirstName && x.LastName == southernADealerSalesStaff3.LastName);
            southernADealerSalesStaff4 = context.Staffs.FirstOrDefault(x => x.FirstName == southernADealerSalesStaff4.FirstName && x.LastName == southernADealerSalesStaff4.LastName);
            southernBDealerSalesStaff1 = context.Staffs.FirstOrDefault(x => x.FirstName == southernBDealerSalesStaff1.FirstName && x.LastName == southernBDealerSalesStaff1.LastName);
            southernBDealerSalesStaff2 = context.Staffs.FirstOrDefault(x => x.FirstName == southernBDealerSalesStaff2.FirstName && x.LastName == southernBDealerSalesStaff2.LastName);
            southernBDealerSalesStaff3 = context.Staffs.FirstOrDefault(x => x.FirstName == southernBDealerSalesStaff3.FirstName && x.LastName == southernBDealerSalesStaff3.LastName);
            southernBDealerSalesStaff4 = context.Staffs.FirstOrDefault(x => x.FirstName == southernBDealerSalesStaff4.FirstName && x.LastName == southernBDealerSalesStaff4.LastName);

            #endregion

            #endregion

            #region Sales handling

            #region Sales Initialization

            sale1 = new Sale()  { DealerID = northernADealer.ID, SaleDate = DateTime.Today,
                StaffID = northernADealerSalesStaff1.ID, VehicleID = vehicle1.ID, ID = 1, SaleValue = 100 };
            sale2 = new Sale()  { DealerID = northernADealer.ID, SaleDate = DateTime.Today,
                StaffID = northernADealerSalesStaff1.ID, VehicleID = vehicle2.ID, ID = 2, SaleValue = 150 };
            sale3 = new Sale()  { DealerID = northernADealer.ID, SaleDate = DateTime.Today,
                StaffID = northernADealerSalesStaff2.ID, VehicleID = vehicle3.ID, ID = 3, SaleValue = 200 };
            sale4 = new Sale()  { DealerID = northernADealer.ID, SaleDate = DateTime.Today,
                StaffID = northernADealerSalesStaff2.ID, VehicleID = vehicle4.ID, ID = 4, SaleValue = 180 };
            sale5 = new Sale()  { DealerID = northernADealer.ID, SaleDate = DateTime.Today.AddDays(3),
                StaffID = northernADealerSalesStaff1.ID, VehicleID = vehicle5.ID, ID = 5, SaleValue = 140 };
            sale6 = new Sale()  { DealerID = northernADealer.ID, SaleDate = DateTime.Today.AddDays(3),
                StaffID = northernADealerSalesStaff1.ID, VehicleID = vehicle6.ID, ID = 6, SaleValue = 190 };
            sale7 = new Sale()  { DealerID = northernADealer.ID, SaleDate = DateTime.Today.AddDays(3),
                StaffID = northernADealerSalesStaff2.ID, VehicleID = vehicle7.ID, ID = 7, SaleValue = 170 };
            sale8 = new Sale()  { DealerID = northernADealer.ID, SaleDate = DateTime.Today.AddDays(3),
                StaffID = northernADealerSalesStaff2.ID, VehicleID = vehicle8.ID, ID = 8, SaleValue = 140 };
            sale9 = new Sale()  { DealerID = northernBDealer.ID, SaleDate = DateTime.Today,
                StaffID = northernBDealerSalesStaff1.ID, VehicleID = vehicle8.ID, ID = 9, SaleValue = 156 };
            sale10 = new Sale() { DealerID = northernBDealer.ID, SaleDate = DateTime.Today,
                StaffID = northernBDealerSalesStaff1.ID, VehicleID = vehicle10.ID, ID = 10, SaleValue = 180 };
            sale11 = new Sale() { DealerID = northernBDealer.ID, SaleDate = DateTime.Today,
                StaffID = northernBDealerSalesStaff2.ID, VehicleID = vehicle11.ID, ID = 11, SaleValue = 190 };
            sale12 = new Sale() { DealerID = northernBDealer.ID, SaleDate = DateTime.Today,
                StaffID = northernBDealerSalesStaff2.ID, VehicleID = vehicle12.ID, ID = 12, SaleValue = 140 };
            sale13 = new Sale() { DealerID = northernBDealer.ID, SaleDate = DateTime.Today.AddDays(3),
                StaffID = northernBDealerSalesStaff1.ID, VehicleID = vehicle13.ID, ID = 13, SaleValue = 130 };
            sale14 = new Sale() { DealerID = northernBDealer.ID, SaleDate = DateTime.Today.AddDays(3),
                StaffID = northernBDealerSalesStaff1.ID, VehicleID = vehicle14.ID, ID = 14, SaleValue = 120 };
            sale15 = new Sale() { DealerID = northernBDealer.ID, SaleDate = DateTime.Today.AddDays(3),
                StaffID = northernBDealerSalesStaff2.ID, VehicleID = vehicle15.ID, ID = 15, SaleValue = 190 };
            sale16 = new Sale() { DealerID = northernBDealer.ID, SaleDate = DateTime.Today.AddDays(3),
                StaffID = northernBDealerSalesStaff2.ID, VehicleID = vehicle16.ID, ID = 16, SaleValue = 160 };
            sale17 = new Sale() { DealerID = southernADealer.ID, SaleDate = DateTime.Today,
                StaffID = southernADealerSalesStaff1.ID, VehicleID = vehicle17.ID, ID = 17, SaleValue = 300 };
            sale18 = new Sale() { DealerID = southernADealer.ID, SaleDate = DateTime.Today.AddDays(3),
                StaffID = southernADealerSalesStaff1.ID, VehicleID = vehicle18.ID, ID = 18, SaleValue = 310 };
            sale19 = new Sale() { DealerID = southernADealer.ID, SaleDate = DateTime.Today,
                StaffID = southernADealerSalesStaff2.ID, VehicleID = vehicle19.ID, ID = 19, SaleValue = 340 };
            sale20 = new Sale() { DealerID = southernADealer.ID, SaleDate = DateTime.Today.AddDays(3),
                StaffID = southernADealerSalesStaff2.ID, VehicleID = vehicle20.ID, ID = 20, SaleValue = 380 };

            #endregion

            #region Sales "Upsert" Operations

            context.Sales.AddOrUpdate(x => x.ID,
                    sale1, sale2, sale3, sale4, sale5, sale6, sale7, sale8, sale9, sale10,
                        sale11, sale12, sale13, sale14, sale15, sale16, sale17, sale18, sale19, sale20);

            #endregion

            #endregion

            #endregion
        }
    }
}
