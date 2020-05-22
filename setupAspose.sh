#!/bin/bash
set -e

export DEBIAN_FRONTEND=noninteractive

# libgdiplus and others need to be installed before sources.list is updated:
apt-get update
apt-get install -y libgdiplus libc6-dev libx11-dev libfontconfig1
apt-get install fontconfig
apt-get install -y xfonts-utils

# update sources so that fonts package is available:
echo "deb http://httpredir.debian.org/debian jessie main contrib" > /etc/apt/sources.list
echo "deb http://security.debian.org/ jessie/updates main contrib" >> /etc/apt/sources.list
apt-get update

# accept license and install fonts, then rebuild font cache
echo "ttf-mscorefonts-installer msttcorefonts/accepted-mscorefonts-eula select true" | debconf-set-selections
apt-get install -y ttf-mscorefonts-installer fonts-opensymbol
fc-cache -fv

# tidy up to minimize layer size (as this script is called in a dockerfile):
apt-get clean
apt-get autoremove -y
rm -rf /var/lib/apt/lists/*


