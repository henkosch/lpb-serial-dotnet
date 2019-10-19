#!/bin/bash

# Publish to Raspberry Pi

dotnet publish -r linux-arm

rsync -r bin/Debug/netcoreapp3.0/linux-arm/publish pi@raspberrypi:~/LpbSerialDotnet

