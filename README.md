# QuickSnap Mobile App


This repository contains a proof of concept mobile app built using .NET MAUI and Blazor Hybrid. It demonstrates the seamless integration between Blazor and native XAML components, offering real-time functionality and cross-platform compatibility.

## Overview

This mobile app showcases the following key features:
- **Real-Time Rendering**: Efficient, real-time updates using Blazor for dynamic content.
- **Navigation Between Blazor & XAML**: Smooth transitions between Blazor and native XAML pages.
- **Taking Pictures**: Capture images directly from the app using the `MediaPicker` and MAUI's Community Toolkit.
- **Web API Integration**: Send and receive data to a web API.
- **Secure Storage**: Store secrets and sensitive data securely within the app.

## Prerequisites

To run this app successfully, ensure that the **QuickSnap Web** and **API** projects are running. You can find those projects here:
[QuickSnap Web and API Projects](https://github.com/celophi/quick-snap-web)

## Getting Started

Follow these steps to set up and run the mobile app:

### Prerequisites
- Make sure you have the following installed:
  - .NET MAUI
  - Visual Studio 2022 (with MAUI workload)
  - MAUI Community Toolkit
  - Access to a camera for MediaPicker functionality

### Running the Application
1. Clone this repository.
    ```bash
    git clone https://github.com/YOUR-USERNAME/quick-snap-mobile.git
    ```

2. Set up and run the **QuickSnap Web** and **API** projects locally by following the instructions in the [QuickSnap Web and API Projects](https://github.com/celophi/quick-snap-web) repository.

3. Build and deploy the mobile app to an emulator or physical device using Visual Studio.

4. When the app starts, you will be prompted to **register an account**. Account registration is required to use the app.

### Key Functionalities
- **Blazor & XAML Navigation**: Experience seamless navigation between Blazor components and native XAML pages.
- **Real-Time Data**: Observe real-time rendering and updates as data changes within the app.
- **Capture Images**: Use the `MediaPicker` to take pictures and upload them to the web API.
- **Secure Data Storage**: Sensitive information is stored securely using MAUI's Secure Storage capabilities.
  
## Notes

- **API Dependencies**: The app depends on the running QuickSnap Web and API projects. Ensure they are correctly configured to communicate with the mobile app.
- **Camera Access**: To capture images, make sure the app has permission to access the device's camera.

## License

This project is licensed under the MIT License.