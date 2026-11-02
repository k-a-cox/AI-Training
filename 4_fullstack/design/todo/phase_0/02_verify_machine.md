# 0.2 Verify Intended Machine

## Prerequisites

Task 0.1 acceptance; access to the OTHER Windows machine intended for Visual Studio and SQL Server. The current workspace host is not assumed to be that machine.

## Learning Goal

Evaluate compatibility, resources, permissions, and installation risk before choosing tooling.

## Allowed Scope

Read-only inventory on the intended host and a user-approved local setup note. No installers, downloads, configuration changes, firewall changes, or elevation attempts.

## Actions

- [x] Ask the learner to confirm the intended hostname before collecting inventory there.
- [x] Record Windows version, CPU architecture, RAM, free disk, and existing .NET/Visual Studio/SQL installations.
- [x] Confirm account/admin policy without requesting passwords or tokens through chat.
- [x] Compare current vendor requirements; budget approximately 32 GB RAM and 80-100 GB free disk for simultaneous BI tools, or stage services on smaller hardware.
- [x] Decide same-host versus trusted LAN/VPN connections for frontend/API/SQL; do not expose SQL publicly.
- [x] Agree which exact components to install in 0.3 and later 2.1.

## Inventory Takeaways

Reported by the learner for the confirmed intended host, `KalebPC`, on 2026-11-02:

- Windows 11 Pro, version 10.0.26200, 64-bit x64; Intel Core i7-1265U, 10 cores / 12 logical processors.
- 31.4 GB RAM and 367.9 GB free on C:.
- .NET SDK 10.0.403 and Visual Studio Community 2026 18.11.12328.437 are already installed. No SQL Server, SQL Agent, SSAS, or Reporting Services services were found.
- The learner reports being able to approve installations and handle elevation prompts; actual account rights were not independently inspected. Selected Visual Studio workloads were not reported.
- Visual Studio 2026 supports targeting .NET 10; its guidance recommends 16 GB for typical professional solutions and says it works best with 64 GB. This host is just under the project's approximate 32 GB combined-stack budget, so simultaneous BI services may need resource limits or staging.
- SQL Server 2022 Developer supports Windows 11 Pro. SSDT's Visual Studio 2026 extensions support SQL Server 2022 project targets. The planned tools are Visual Studio Community/.NET 10 (already present), then SQL Server 2022 Developer, SSMS, SSDT with SSIS/SSAS/SSRS extensions, and standard Power BI Desktop.
- The learner chose a same-host frontend/API/SQL topology, with BI services staged and SQL Server memory limited as needed. Keep development connections local; do not expose SQL publicly. No installations or configuration changes were made.

Vendor references:

- [Visual Studio 2026 system requirements](https://learn.microsoft.com/en-us/visualstudio/releases/2026/vs-system-requirements)
- [.NET SDK and Visual Studio version compatibility](https://learn.microsoft.com/en-us/dotnet/core/porting/versioning-sdk-msbuild-vs)
- [SQL Server 2022 hardware and software requirements](https://learn.microsoft.com/en-us/sql/sql-server/install/hardware-and-software-requirements-for-installing-sql-server-2022)
- [SSDT installation and extension compatibility](https://learn.microsoft.com/en-us/sql/ssdt/download-sql-server-data-tools-ssdt)

## Verification and Acceptance

Host identity, inventory, and development connection plan have been recorded, and task 0.1 acceptance was confirmed. The learner still owns task status and acceptance. Agreeing on components and topology is not installation authorization. If remote access is unavailable, have the learner run approved read-only inventory commands there and provide non-sensitive results. Stop before installing anything.
