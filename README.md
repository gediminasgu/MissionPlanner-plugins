# MissionPlanner-plugins
Collection of different plugins for Mission Planner.

[![Build status](https://ci.appveyor.com/api/projects/status/ynfyolp87i0cipt5?svg=true)](https://ci.appveyor.com/project/gediminasgu/missionplanner-plugins)

Tested MP versions:
- 1.3.48

## How to install?
Just copy paste according plugin dll files to your Mission Planner's Plugins folder.

## Plugins
### RollPitchGimbal
Mission Planner calculates correctly where gimbal is looking at (lat/lng) only if gimbal uses both YAW and PITCH control. But if gimbal uses only ROLL and PITCH control then Mission Planner doesn't provide correct latitude and longitude. This plugin fixes that. [More about this plugin](MissionPlanner.Plugins.RollPitchGimbal/README.md)

### RoiTracking
This plugin presents a possibility to track interesting points where gimbal is looking to. [More about this plugin](MissionPlanner.Plugins.RoiTracking/README.md)
