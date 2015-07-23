public class STATS {
  // Reporting
  // | - Categories
  public const string GAME_STATISTICS       = "Game Statistics";

  // | - Actions
  public const string STATISTICS_ON_SCENE   = "Statistics on Scene";
  public const string STATISTICS_ON_LAUNCH  = "Statistics on Launch";

  // | - Labels
  // | | - First Launch Stats
  public const string DATE_FIRST_LAUNCH     = "Date of First Launch";
  public const string SECONDS_SINCE_INSTALL = "Seconds Since Install";
  public const string DAYS_SINCE_INSTALL    = "Days Since Install";
  public const string FIRST_VERSION         = "First Version";
  public const string INSTALL               = "Install Event";

  // | | - Subsequent Launch Stats
  public const string DATE_LAST_UPDATE      = "Date of Last Update";
  public const string SECONDS_SINCE_UPDATE  = "Seconds Since Last Update";
  public const string DAYS_SINCE_UPDATE     = "Days Since Last Update";
  public const string UPDATED               = "Update Event - Days Between Updates";
  public const string LAST_VERSION          = "Current Version";
  public const string TOTAL_LAUNCH_COUNT    = "Total Number of Launches";
  public const string DATE_LAST_LAUNCH      = "Date of Most Recent Launch";

  // | | - Play stats
  public const string TOTAL_DONUTS_SHOT     = "Total Donuts Shot";
  public const string PEAK_DONUTS_SHOT      = "Peak Donuts Shot";
  // | | | - Orc Kills
  public const string TOTAL_ORCS_KILLED     = "Total Orcs Killed";
  public const string PEAK_ORCS_KILLED      = "Peak Orcs Killed";
  // | | | - Distance
  public const string PEAK_DISTANCE_RUN     = "Peak Distance Run";
  public const string TOTAL_DISTANCE_RUN    = "Total Distance Run";
  // | | | - Cash
  public const string PEAK_CASH_EARNED      = "Peak Cash Earned";
  public const string TOTAL_CASH_EARNED     = "Total Cash Earned";
  // | | | - Jumps
  public const string TOTAL_FIRST_JUMPS     = "Total Times JumpCheck=0";
  public const string PEAK_FIRST_JUMPS      = "Peak Times JumpCheck=0";
  public const string TOTAL_SECOND_JUMPS    = "Total Times JumpCheck=1";
  public const string PEAK_SECOND_JUMPS     = "Peak Times JumpCheck=1";
  public const string TOTAL_THIRD_JUMPS     = "Total Times JumpCheck=2";
  public const string PEAK_THIRD_JUMPS      = "Peak Times JumpCheck=2";

  // | | - Usage
  public const string TOTAL_DEATHS          = "Total Deaths";
  public const string TOTAL_TIME_PLAYED     = "Total Time Played";
  public const string ENDLESS_RUN_TIME      = "Total Endless Run Time";
  public const string SCENE_PREFIX          = "Loaded Scene ";
  public const string SESSION_TIME_ELAPSED  = "Session Time Elapsed";
  public const string TOTAL_UPDATES         = "Total Updates";

  public static string[] getAllStatsKeys() {
    return new string[] {
      DATE_FIRST_LAUNCH,
      SECONDS_SINCE_INSTALL,
      FIRST_VERSION,
      DATE_LAST_UPDATE,
      SECONDS_SINCE_UPDATE,
      LAST_VERSION,
      TOTAL_LAUNCH_COUNT,
      DATE_LAST_LAUNCH,
      TOTAL_DONUTS_SHOT,
      PEAK_DONUTS_SHOT,
      TOTAL_ORCS_KILLED,
      PEAK_ORCS_KILLED,
      PEAK_DISTANCE_RUN,
      TOTAL_DISTANCE_RUN,
      PEAK_DISTANCE_RUN,
      TOTAL_DISTANCE_RUN,
      PEAK_CASH_EARNED,
      TOTAL_CASH_EARNED,
      TOTAL_FIRST_JUMPS,
      PEAK_FIRST_JUMPS,
      TOTAL_SECOND_JUMPS,
      PEAK_SECOND_JUMPS,
      TOTAL_THIRD_JUMPS,
      PEAK_THIRD_JUMPS,
      TOTAL_DEATHS,
      TOTAL_TIME_PLAYED,
      ENDLESS_RUN_TIME,
      SESSION_TIME_ELAPSED,
      TOTAL_UPDATES,
    };
  }
}
