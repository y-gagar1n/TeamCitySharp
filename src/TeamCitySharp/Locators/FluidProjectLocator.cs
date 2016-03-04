namespace TeamCitySharp.Locators
{

    public class FluidProjectLocator : IProjectLocator
    {

        #region Constructors

        private FluidProjectLocator()
            : base()
        {
        }

        #endregion

        #region Properties

        public string Id
        {
            get;
            private set;
        }

        public string Name
        { 
            get; 
            private set; 
        }

        #endregion

        #region Fluid Methods

        public static FluidProjectLocator WithId(string id)
        {
            return new FluidProjectLocator { Id = id };
        }

        public static FluidProjectLocator WithName(string name)
        {
            return new FluidProjectLocator { Name = name };
        }

        #endregion

        #region Object Methods

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(Id))
            {
                return "id:" + Id;
            }
            if (!string.IsNullOrEmpty(Name))
            {
                return "name:" + Name;
            }
            return string.Empty;
        }

        #endregion

    }

}
