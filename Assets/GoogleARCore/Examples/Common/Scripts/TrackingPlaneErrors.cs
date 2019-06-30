namespace DefaultNamespace
{
    public enum TrackingPlaneErrors
    {
        /// <summary>
        /// Motion tracking is working properly.
        /// </summary>
        None = 0,

        /// <summary>
        /// An internal error is causing motion tracking to fail.
        /// </summary>
        BadState = 1,

        /// <summary>
        /// The camera feed being too dark is causing motion tracking to fail.
        /// </summary>
        InsufficientLight = 2,

        /// <summary>
        /// Excessive movement of the device camera is causing motion tracking
        /// to fail.
        /// </summary>
        ExcessiveMotion = 3,

        ///<summary>
        ///Just enable the plane tracker when the device is pointing down
        /// </summary>>
        IncorrectDevicePosition = 12,
        
        /// <summary>
        /// A lack of visually distinct environmental features in the camera feed
        /// is causing motion tracking to fail.
        /// </summary>
        InsufficientFeatures = 4,
    }
}