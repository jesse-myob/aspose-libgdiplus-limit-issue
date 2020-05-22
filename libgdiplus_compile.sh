#!/bin/bash
set -e

export DEBIAN_FRONTEND=noninteractive

# modified libgdiplus
apt-get update
apt-get install -y libgif-dev autoconf libtool automake build-essential gettext libglib2.0-dev libcairo2-dev libtiff-dev libexif-dev git-core
git clone https://github.com/jesse-myob/libgdiplus.git

cd libgdiplus

./autogen.sh --prefix=/libgdiplus/mono
make

make install
