#!/bin/bash

cd /opt/FlightNode

pushd packages
nuget install xunit.runner.console
popd

xunit=packages/xunit.runner.console.2.1.0/tools/xunit.console.exe
common=FlightNode.Common.UnitTests
dc=FlightNode.DataCollection.UnitTests
identity=FlightNode.Identity.UnitTests
api=FlightNode.Common.Api.UnitTests

mono $xunit test/$common/bin/Debug/$common.dll
mono $xunit test/$dc/bin/Debug/FlightNode.DataCollection.Domain.UnitTests.dll
mono $xunit test/$identity/bin/Debug/$identity.dll
mono $xunit test/$api/bin/Debug/FligthNote.Common.Api.UnitTests.dll
