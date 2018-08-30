### Author: Mattsi Jansky
### Description: Dungeons of Mericost
### Category: Game
### License: MIT
### Appname: mericost
### Built-in: no

import badge
import ugfx
import deepsleep
import wifi
import time
import sys

sys.path.append('/lib/Network_test')
import network_lib

newCharacterRequest = {"Archetype":"0", "Name":"test","Race":"0"}

def network_test():
    badge.eink_init()
    ugfx.init()
    ugfx.input_init()
    wifi_up()
    cleanScreen()
    ugfx.input_attach(ugfx.BTN_START, reboot)
    ugfx.text(10, 10, "Testing network", ugfx.BLACK)
    ugfx.flush()

    #Test POST
    result = network_lib.sendPOST("https://18.188.162.122/Player/New", newCharacterRequest, debug = True)

def cleanScreen():
    ugfx.clear(ugfx.WHITE)
    ugfx.flush()

def reboot(wut):
    deepsleep.reboot()

def wifi_up():
  wifi.init()
  while not wifi.sta_if.isconnected():
    time.sleep(0.1)
    pass
  return wifi.sta_if.ifconfig()[0]

network_test()