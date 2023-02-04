namespace ReadApi.Application.Services
{
    public interface IViolationService
    {
        /// <summary>
        /// Calculate the vehicle speed
        /// </summary>
        /// <param name="distanceMeter">Enter the distance in meters</param>
        /// <param name="firstSystemReadTime">Enter the readtimestamp from the first system in Unix time seconds. 
        /// </param>
        /// <param name="secondSystemReadTime">Enter the readtimestamp from the second system in Unix time seconds.</param>
        /// <returns></returns>
        double CalculateSpeed(double distanceMeter, long firstSystemReadTime, long secondSystemReadTime);
    }
}