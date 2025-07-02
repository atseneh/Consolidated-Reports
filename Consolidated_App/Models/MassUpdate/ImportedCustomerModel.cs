namespace Consolidated_App.Models
{
    public class ImportedCustomerModel
    {
        public string organizationID { get; set; }
        public string organizationName { get; set; }
        public string businessType { get; set; }
        public string tin { get; set; }
        public string telephone { get; set; }
        public string city { get; set; }
        public string street { get; set; }
        public string category { get; set; }
        public string parentCategory { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }


    }
    public class MassUpdateForImport
    {
        public List<ImportedCustomerModel> ExcelCountList { get; set; }
        public IFormFile fileUpload { get; set; }
        public string distributorName { get; set; }
        public string distributorID { get; set; }
        public string selectedDisConnectionString { get; set; }
        public string selectedDistributor { get; set; }
        public MassUpdateStatusInformation messages { get; set; }

    }

    public class OutletTransferModel
    {
        public string clientScreenHeight { get; set; }
        public string clientScreenWidth { get; set; }
        public string sourceDistributor { get; set; }
        public string selectedconnDisplay { get; set; }
        public string selectedRecord { get; set; }
        public string selectedAction { get; set; }
        public string actionDestinations { get; set; }
        public string moveorCopy { get; set; }
        public string destinationDistributor { get; set; }
        public MassUpdateStatusInformation messages { get; set; }

        public string code { get; set; }
        public string cusName { get; set; }
        public string sector { get; set; }
        public string category { get; set; }
        public string selected { get; set; }
        public bool isActive { get; set; }
        public string filtercriatria { get; set; }
        public string filt_sector { get; set; }
        public string filt_category { get; set; }
        public string filt_semid { get; set; }
        public string filt_name { get; set; }
    }
    public class saveStatus
    {
        public string saved { get; set; }
        public string failed { get; set; }
        public string reason { get; set; }
    }
}
