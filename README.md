### Paradox Stand-alone launcher library integration for Playnite

As of v0.2 (current release), the extension can:
- add CKIII, Battletech and Stellaris into your games library regardless of ownership
- properly detect install status of all three games AND the launcher itself (due to static games folder placement in PDX Launcher)
- properly run all three games AND the launcher itself (including running Launcher v2 for fetching mods, where applicable)
- properly attempt to install/uninstall all three games through launcher
- interact with non-Paradox based metadata sources to fetch games' metadata
- overall, interact with ALL third-party plugins, such as DuplicateHider by FelixKMH, just fine

Goals for 1.0:
- ownership based adding to library
- dynamically generated games list to iterate through
- proper install/uninstall support for both launcher and game list
- overall, getting rid of spaghetti

### Caveat Emptor

Extension's codebase is, essentially, a big bowl of spaghetti which just works.

Good enough for code made by person which is incompatible with programming on genetic level.

Exercise discretion prior to installing.

-- Aliaksandr
