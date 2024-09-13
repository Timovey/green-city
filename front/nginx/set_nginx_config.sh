#!/bin/bash
rm default.conf
cp default2.conf default.conf
rm default2.conf
nginx -s reload