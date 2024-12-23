# Carrot-Ads

Carrot-Ads is a comprehensive advertising management system designed specifically for Unity3D projects. It provides seamless integration of various ad networks and custom-designed ads across multiple platforms, including PC, Android, and iOS.

---

## Key Features

### 🛠 Multi-Platform Support
- **PC, Android, iOS**: Unified solution for all major platforms.
- Designed for optimal performance and compatibility with Unity3D.

### 📢 Integrated Ad Networks
- **AdMob**: Effortlessly integrate Google's advertising platform.
- **Unity Ads**: Take advantage of Unity's native ad solution.
- **Other Ad Networks**: Easily incorporate additional advertising platforms.

### 🎨 Customizable Ads
- Create and manage **custom-designed advertisements** tailored to your app or game.
- Full support for **interactive ad formats** to enhance user engagement.

### ⚙️ Developer-Friendly
- Simple and intuitive APIs for developers.
- Detailed documentation to get you started quickly.
- Examples and templates to streamline ad integration.

---

## Installation

1. **Clone or Download the Repository**
   ```bash
   git clone https://github.com/yourusername/Carrot-Ads.git
   ```
2. **Import into Unity**
   - Open Unity and import the `Carrot-Ads` package into your project.

3. **Configure Ad Networks**
   - Follow the setup guide for integrating **AdMob**, **Unity Ads**, or any other networks of your choice.

---

## Getting Started

1. **Initialize Carrot-Ads**
   ```csharp
   using CarrotAds;

   void Start() {
       CarrotAdsManager.Initialize();
   }
   ```

2. **Display Ads**
   ```csharp
   CarrotAdsManager.ShowAd("ad_unit_id");
   ```

3. **Customize Ads**
   ```csharp
   CarrotAdsManager.CreateCustomAd("Custom Ad Title", "Custom Ad Content", "ImagePath");
   ```

---

## Customization

Carrot-Ads supports full customization to align with your branding and user experience:

- **Ad Design**: Upload custom images and videos.
- **Ad Placement**: Easily configure ad placement and timing.
- **User Interaction**: Add clickable elements or animations.

---

## Contributions

We welcome contributions to make Carrot-Ads even better. To contribute:

1. Fork the repository.
2. Create a new branch for your feature or bugfix.
3. Submit a pull request with a detailed description.

---

## License

Carrot-Ads is licensed under the [MIT License](LICENSE).

---

## Support

For questions, feedback, or feature requests, contact us at:
- **Email**: kurotsmile@mail.com
- **GitHub Issues**: Submit an issue on our [GitHub repository](https://github.com/yourusername/Carrot-Ads/issues).
