# 🎿 Lab2_G1_SnowBoarder

## 📖 Tổng quan Game

**Lab2_G1_SnowBoarder** là một game trượt tuyết 2D được phát triển bằng Unity, nơi người chơi điều khiển một snowboarder trượt xuống các sườn núi đầy thử thách. Game kết hợp vật lý thực tế với gameplay thú vị, yêu cầu kỹ năng cân bằng và timing để đạt điểm cao.

---

## 🎮 Tính năng chính

### 🏂 **Gameplay Core**

- **Vật lý thực tế**: Trượt dựa trên độ dốc, trọng lực và ma sát
- **Hệ thống cân bằng**: Giữ thăng bằng trên các bề mặt nghiêng
- **Xoay 360°**: Thực hiện các trick trên không để kiếm điểm bonus
- **Bám thẳng đứng**: Khả năng bám vào các bề mặt dốc đứng

### 🎯 **Hệ thống điểm số**

- **Điểm khoảng cách**: Tăng theo vị trí xa nhất đạt được
- **Bonus xoay**: 100 điểm cho 1 vòng, 150 điểm cho mỗi vòng tiếp theo
- **Thu thập items**: Các item vàng cho điểm cố định
- **Hệ thống sao**: 1-3 sao dựa trên điểm đạt được

### 🛡️ **Hệ thống bảo vệ**

- **Khiên tạm thời**: Bảo vệ khỏi 1 lần va chạm
- **Khiên vĩnh cửu**: Bất tử hoàn toàn (cheat mode)
- **Miễn nhiễm**: Bảo vệ khỏi tất cả chướng ngại vật

---

## 🎮 Điều khiển

### ⌨️ **Phím cơ bản**

| Phím      | Chức năng                        |
| --------- | -------------------------------- |
| **Space** | Nhảy / Bám vào bề mặt thẳng đứng |
| **W/S**   | Xoay trái/phải trên không        |
| **A/D**   | Di chuyển trái/phải (God Mode)   |

### 🚀 **God Mode (Cheat)**

| Phím          | Chức năng              |
| ------------- | ---------------------- |
| **Shift + B** | Bật/tắt bay (nhấn giữ) |
| **W/S**       | Tiến/lùi theo hướng    |
| **Q/E**       | Lên/xuống              |
| **A/D**       | Trái/phải              |
| **Z/X**       | Xoay trái/phải         |

---

## 🎯 Hệ thống Level

### 📊 **Các Level và Điểm sao**

| Level       | 1⭐   | 2⭐   | 3⭐   |
| ----------- | ----- | ----- | ----- |
| **Level 1** | 2,000 | 3,000 | 3,600 |
| **Level 2** | 5,400 | 6,400 | 7,200 |
| **Level 3** | 5,400 | 6,900 | 8,500 |
| **Level 4** | 4,000 | 5,000 | 6,000 |
| **Level 5** | 4,500 | 5,500 | 6,500 |

### 🔓 **Mở khóa Level**

- **Level 2**: Hoàn thành Level 1
- **Level 3**: Hoàn thành Level 2
- **Level 4**: Hoàn thành Level 3
- **Level 5**: Hoàn thành Level 4

---

## 🎁 Items & Power-ups

### 💎 **Items tích cực**

| Item              | Hiệu ứng     | Thời gian |
| ----------------- | ------------ | --------- |
| **Speed Boost**   | Tăng tốc 20x | 3 giây    |
| **Score Item**    | +100 điểm    | Tức thì   |
| **Helmet Shield** | Bảo vệ 1 lần | 5 giây    |

### ⚠️ **Chướng ngại vật**

| Item               | Hiệu ứng     | Bảo vệ                 |
| ------------------ | ------------ | ---------------------- |
| **Wood**           | Giảm tốc 50% | Có thể chặn bằng khiên |
| **Destroy**        | Game Over    | Cần khiên để sống      |
| **Fall Zone**      | Game Over    | Cần khiên vĩnh cửu     |
| **Crash Detector** | Game Over    | Cần khiên vĩnh cửu     |

---

## 🎛️ Cheat System

### 🔧 **Kích hoạt Cheat Mode**

```
CTRL + C: Bật/tắt cheat mode
```

### 🚀 **Movement Cheats**

| Phím             | Chức năng   | Hiệu ứng        |
| ---------------- | ----------- | --------------- |
| **CTRL+SHIFT+S** | Super Speed | Tốc độ x2       |
| **CTRL+SHIFT+J** | Mega Jump   | Nhảy x3         |
| **CTRL+SHIFT+N** | No-Clip     | Bay qua vật thể |

### 🛡️ **Player State Cheats**

| Phím             | Chức năng       | Hiệu ứng       |
| ---------------- | --------------- | -------------- |
| **CTRL+SHIFT+G** | God Mode        | Bất tử + bay   |
| **CTRL+SHIFT+H** | Infinite Shield | Khiên vĩnh cửu |
| **CTRL+SHIFT+I** | Instant Shield  | Tạo khiên ngay |

### 📊 **Score & Progression Cheats**

| Phím             | Chức năng       | Hiệu ứng        |
| ---------------- | --------------- | --------------- |
| **CTRL+SHIFT+P** | Add Points      | +1000 điểm      |
| **CTRL+SHIFT+R** | Reset Score     | Reset điểm về 0 |
| **CTRL+SHIFT+F** | Teleport Finish | Nhảy đến đích   |

---

## ⚙️ Cài đặt kỹ thuật

### 🎮 **Thông số vật lý**

```csharp
Max Speed: 35f
Jump Force: 20f
Gravity Scale: 3.0f (2f * 1.5f)
Mass: 2f
Linear Damping: 0.001f
Angular Damping: 0.5f
```

### 🎯 **Thông số cân bằng**

```csharp
Balance Force: 2f
Max Balance Angle: 60°
Balance Speed: 25f
Stability Force: 2f
```

### 🚀 **Thông số bay**

```csharp
Air Rotation Speed: 720°/s
Max Air Rotation: 500°/s
God Mode Fly Speed: 20f
God Mode Max Speed: 100f
```

---

## 📁 Cấu trúc Project

### 🎬 **Scenes**

```
Assets/Scenes/
├── GameMenu.unity      # Menu chính
├── Level1.unity        # Level 1
├── Level2.unity        # Level 2
└── Level3.unity        # Level 3
```

### 📜 **Scripts chính**

```
Assets/Scrips/
├── PlayerController.cs         # Điều khiển nhân vật
├── GameManager.cs              # Quản lý game
├── ScoreManager.cs             # Hệ thống điểm
├── CheatManager.cs             # Hệ thống cheat
├── AudioManager.cs             # Âm thanh
├── GlobalScoreManager.cs       # Lưu điểm toàn cục
└── LevelCompletionChecker.cs   # Kiểm tra hoàn thành level
```

### 🎁 **Items & Effects**

```
Assets/Scrips/
├── SpeedItem.cs                # Item tăng tốc
├── ScoreItem.cs                # Item điểm
├── HelmetItem.cs               # Item khiên
├── WoodItem.cs                 # Chướng ngại gỗ
├── DestroyItem.cs              # Chướng ngại nguy hiểm
├── FallZone.cs                 # Vùng rơi
├── CrashDetector.cs            # Phát hiện va chạm
└── DustTrail.cs                # Hiệu ứng tuyết
```

---

## 🎵 Audio System

### 🎶 **Nhạc nền**

- **Menu Music**: Nhạc chủ đề
- **Game Music**: Nhạc trong game
- **Win Music**: Nhạc chiến thắng
- **Lose Music**: Nhạc thua cuộc

### 🔊 **Sound Effects**

- **Button Click**: Âm thanh nút bấm
- **Get Stars**: Thu thập sao
- **Get Shields**: Nhận khiên
- **Get Hurt**: Bị thương
- **Get Wood**: Va chạm gỗ
- **Shield Disappear**: Khiên biến mất

---

## 💾 Save System

### 📊 **Dữ liệu lưu trữ**

```json
{
  "highScores": {
    "Level1": 2500.0,
    "Level2": 3000.0,
    "Level3": 4000.0
  },
  "completedLevels": {
    "Level1": true,
    "Level2": true,
    "Level3": false
  }
}
```

### 📍 **Vị trí lưu file**

- **Windows**: `C:\Users\[Username]\AppData\LocalLow\DefaultCompany\Lab2_G1_SnowBoarder\highscores.json`
- **WebGL**: PlayerPrefs với key `"highscores_json"`

---

## 🎨 Visual Effects

### ❄️ **Hiệu ứng tuyết**

- **Dust Trail**: Vệt tuyết khi trượt
- **Snow Generator**: Tuyết rơi môi trường
- **Particle Systems**: Hiệu ứng hạt

### 🎭 **Animations**

- **Sliding**: Trượt trên mặt đất
- **Jumping**: Nhảy trên không
- **Sticking**: Bám vào bề mặt thẳng đứng
- **Flying**: Bay trong God Mode

---

## 🚀 Build & Deploy

### 📦 **Build Settings**

```
Scenes in Build:
0. GameMenu
1. Level1
2. Level2
3. Level3
```

### 🌐 **Platform Support**

- **Windows**: Standalone
- **WebGL**: Browser
- **Android**: Mobile (có thể)

---

## 🐛 Debug & Testing

### 🔧 **Debug Features**

- **Console Logs**: Chi tiết hoạt động
- **Debug Mode**: Hiển thị thông tin debug
- **Cheat System**: Test các tính năng
- **Performance**: FPS và memory usage

### 📊 **Performance**

- **Target FPS**: 60 FPS
- **Memory Usage**: < 100MB
- **Build Size**: < 50MB

---

## 👥 Credits

### 🎮 **Development**

- **Engine**: Unity 2022.3 LTS
- **Language**: C#
- **Platform**: 2D Physics

### 🎨 **Assets**

- **Graphics**: Custom sprites
- **Audio**: Custom sound effects
- **Animations**: Unity Animator

---

## 📝 Changelog

### 🆕 **Version 1.0**

- ✅ Core gameplay mechanics
- ✅ 3 levels với độ khó tăng dần
- ✅ Hệ thống điểm số và sao
- ✅ Cheat system đầy đủ
- ✅ Save/Load system
- ✅ Audio system hoàn chỉnh

---

## 🎯 Mục tiêu Game

### 🏆 **Điểm cao**

- Thu thập tối đa điểm từ khoảng cách
- Thực hiện nhiều trick xoay
- Thu thập tất cả items
- Hoàn thành level với 3 sao

### 🎮 **Kỹ năng cần thiết**

- **Timing**: Nhảy đúng lúc
- **Balance**: Giữ thăng bằng
- **Strategy**: Sử dụng items hiệu quả
- **Memory**: Ghi nhớ vị trí chướng ngại

---

## 📞 Support

### 🐛 **Báo lỗi**

Nếu gặp lỗi, vui lòng báo cáo với thông tin:

- Mô tả lỗi
- Các bước tái tạo
- Screenshot (nếu có)

### 💡 **Góp ý**

Mọi góp ý về gameplay, UI/UX đều được chào đón!

---

**🎿 Chúc bạn chơi game vui vẻ! 🎿**
