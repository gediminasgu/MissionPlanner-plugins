# MissionPlanner-plugins
Collection of different plugins for Mission Planner.

## How to install?
Just copy paste according plugin dll files to your Mission Planner's Plugins folder.

## Plugins
### RollPitchGimbal
Mission Planner calculates correctly where gimbal is looking at (lat/lng) only if gimbal uses both YAW and PITCH control. But if gimbal uses only ROLL and PITCH control then Mission Planner doesn't provide correct latitude and longitude. This plugin fixes that. [More about this plugin](MissionPlanner.Plugins.RollPitchGimbal/README.md)

### RoiTracking
This plugin presents a possibility to track interesting points where gimbal is looking to. [More about this plugin](MissionPlanner.Plugins.RoiTracking/README.md)

Shortkeys:
- Alt + L - add the point where gimbal is looking to, to the tracking list and start loiter around it.
- Alt + T - just add the the point where gimbal is looking to, to the tracking list.
- Alt + R - resume loiter around the last point.

Also you can see the list choosing the "ROI list" from the context menu.