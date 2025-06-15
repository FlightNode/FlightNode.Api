#!/bin/bash

cd /opt/FlightNode
nuget restore FlightNode.Api.sln
msbuild FlightNode.Api.sln -m
