docker run -it --rm `
    -v $PSScriptRoot/src:/opt/FlightNode/src/:rw `
    -v $PSScriptRoot/test:/opt/FlightNode/test/:rw `
    -v $PSScriptRoot/packages:/opt/FlightNode/packages:rw `
    -v $PSScriptRoot/FlightNode.Api.sln:/opt/FlightNode/FlightNode.Api.sln:rw `
    -v $PSScriptRoot/docker-scripts:/opt/FlightNode/scripts:ro `
    mono:6.0.0.334@sha256:ffd791fd085cf5e782cdf27ad37e7ef3b302f4c7062c7ba2465cfe60590bd52a

# Use as runtime base layer if create a Dockerfile:
# mono:6.0.0-slim@sha256:93a9e125fe75c59e7f91c52fa7015c349b646ca27806e09e613dd58fb5d5b5a2

# Note: previously tried the latest mono, but ran into a cryptography error.
# Tried downgrading to 6.0 after seeing a SO comment that this error came up
# first in 6.4, but it did not solve the problem. Perhaps 4.x? But not worth the
# effort to solve this now.
