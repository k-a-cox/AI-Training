# 0.2 Verify Intended Machine

## Prerequisites

Task 0.1 acceptance; access to the OTHER Windows machine intended for Visual Studio and SQL Server. The current workspace host is not assumed to be that machine.

## Learning Goal

Evaluate compatibility, resources, permissions, and installation risk before choosing tooling.

## Allowed Scope

Read-only inventory on the intended host and a user-approved local setup note. No installers, downloads, configuration changes, firewall changes, or elevation attempts.

## Actions

- [ ] Ask the learner to confirm the intended hostname before collecting inventory there.
- [ ] Record Windows version, CPU architecture, RAM, free disk, and existing .NET/Visual Studio/SQL installations.
- [ ] Confirm account/admin policy without requesting passwords or tokens through chat.
- [ ] Compare current vendor requirements; budget approximately 32 GB RAM and 80-100 GB free disk for simultaneous BI tools, or stage services on smaller hardware.
- [ ] Decide same-host versus trusted LAN/VPN connections for frontend/API/SQL; do not expose SQL publicly.
- [ ] Agree which exact components to install in 0.3 and later 2.1.

## Verification and Acceptance

The learner confirms host identity, compatibility, disk/RAM budget, and permitted installations. If remote access is unavailable, have the learner run approved read-only inventory commands there and provide non-sensitive results. Stop before installing anything.
