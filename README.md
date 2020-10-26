# YTMDesktop App StreamDeck Plugin

## How it works ?

The plugin is using the REST API provided by the YTMDesktop app. Its documentation can be found here:
[YtmDesktop REST Api](https://github.com/ytmdesktop/ytmdesktop/wiki/Remote-Control-API)

To this date (2020-10-26), here are the list of available commands and their current status in the plugin:

|Rest Command|Plugin Support|
|---|---|
|`track-play`|Yes|
|`track-pause`|Yes|
|`track-next`|Yes|
|`track-previous`|Yes|
|`track-thumbs-up`|Yes|
|`track-thumbs-down`|Yes|
|`player-volume-up`|Planned|
|`player-volume-down`|Planned|
|`player-repeat`|Planned|
|`player-shuffle`|Planned|
|`player-add-library`|Planned|
|`player-forward`|Not Planned|
|`player-rewind`|Not Planned|
|`show-lyrics-hidden`|Not Planned|
|`player-add-playlist`|Not Planned|
|`player-set-seekbar`|Not Planned|
|`player-set-volume`|Not Planned|
|`player-set-queue`|Not Planned|

>Why the "Not Planned" ?

Well, remember that you're using a StreamDeck, and its main purpose is to automate stuff. I'm not sure
to see any value in `show-lyrics-hidden` from the StreamDeck. But if you've compeling arguments for some
that are a "must have" to you, please don't hesitate to create an issue, we'll debate over it. 

## Credits
Bootstrapping code by [csharpfritz](https://github.com/csharpfritz) from project [StreamDeckToolkit](https://github.com/FritzAndFriends/StreamDeckToolkit)  
[YTMDesktop](https://github.com/ytmdesktop/ytmdesktop) project.

## Disclaimer

I'm not much of a graphic designer, so I did what I could with the icons... Sorry.