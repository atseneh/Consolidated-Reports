namespace Consolidated_App.Models.FramworkModels
{
    /// <summary>
    /// Represents base nopCommerce entity model
    /// </summary>
    public partial class BaseNopEntityModel : BaseNopModel
    {
        /// <summary>
        /// Gets or sets model identifier
        /// </summary>
        public virtual int Id { get; set; }
    }
}