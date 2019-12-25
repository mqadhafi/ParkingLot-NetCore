namespace ParkingLot.Model
{
    public static class Command
    {
        /// <summary>
        /// This command is used to initialize an empty parking lot
        /// </summary>
        public const string CREATE = "create_parking_lot";

        /// <summary>
        /// This command is used to store car on the parking lot
        /// </summary>
        public const string PARK = "park";

        /// <summary>
        /// This command is used to release car from the parking lot
        /// </summary>
        public const string LEAVE = "leave";

        /// <summary>
        /// This command is used to get status of all cars inside the parking lot
        /// </summary>
        public const string STATUS = "status";

        /// <summary>
        /// This command is used to get all registration numbers by car's colour
        /// </summary>
        public const string REGISTRATION_NUMBERS_WITH_COLOUR = "registration_numbers_for_cars_with_colour";

        /// <summary>
        /// This command is used to get slot numbers for car by car's colour
        /// </summary>
        public const string SLOT_NUMBERS_WITH_COLOUR = "slot_numbers_for_cars_with_colour";

        /// <summary>
        /// This command is used to get slot numbers for car by car's registration number
        /// </summary>
        public const string SLOT_NUMBERS_FOR_REGISTRATION_NUMBER = "slot_number_for_registration_number";
    }
}