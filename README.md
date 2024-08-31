# HealthCheck-Demo
How to implement API health check in dotnet.

## Features
1. Multiple checks for different dependencies.
1. Returning `Degraded` response instead of `Unhealthy` for non-critical deps.
1. Returning status for each dependency.
1. Returning error details.
1. Returning custom data.
1. Filtering health checks based on Tags, for specific endpoints.
1. Kubernetes readiness and liveness probs.

## How to run
1. Clone repo.
1. Install `dotnet 8` sdk if not installed.
1. Run `dotnet run` command.
1. Once running hit [http://localhost:5181/health](`http://localhost:5181/health`) or [http://localhost:5181/health/ready](http://localhost:5181/health/ready) in browser.

### Sample response
```
{
    "status": "Unhealthy",
    "totalDuration": "00:00:00.0104583",
    "entries": {
        "basic_check": {
            "data": {},
            "description": "A healthy result.",
            "duration": "00:00:00.0006799",
            "status": "Healthy",
            "tags": []
        },
        "advance_check": {
            "data": {
                "SomeData": "value1",
                "OtherData": "value2"
            },
            "description": "An unhealthy result.",
            "duration": "00:00:00.0007156",
            "exception": "This is exception",
            "status": "Degraded",
            "tags": []
        },
        "startup": {
            "data": {},
            "description": "That startup task is still running.",
            "duration": "00:00:00.0010888",
            "status": "Unhealthy",
            "tags": [
                "ready"
            ]
        }
    }
}
```