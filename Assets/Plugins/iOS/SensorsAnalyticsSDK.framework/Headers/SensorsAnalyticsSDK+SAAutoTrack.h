//
// SensorsAnalyticsSDK+SAAutoTrack.h
// SensorsAnalyticsSDK
//
// Created by wenquan on 2021/4/2.
// Copyright © 2015-2022 Sensors Data Co., Ltd. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//

#import "SensorsAnalyticsSDK.h"
#import <UIKit/UIKit.h>

NS_ASSUME_NONNULL_BEGIN

/**
 * @abstract
 * 自动追踪 (AutoTrack) 中，实现该 Protocal 的 Controller 对象可以通过接口向自动采集的事件中加入属性
 *
 * @discussion
 * 属性的约束请参考 track:withProperties:
 */
@protocol SAAutoTracker <NSObject>

@required
- (NSDictionary *)getTrackProperties;

@end

@protocol SAScreenAutoTracker <SAAutoTracker>

@optional
- (BOOL)isIgnoredAutoTrackViewScreen;
- (NSString *)getScreenUrl;

@end

#pragma mark -

@interface SensorsAnalyticsSDK (SAAutoTrack)

- (UIViewController *_Nullable)currentViewController;

/**
 * @abstract
 * 是否开启 AutoTrack
 *
 * @return YES: 开启 AutoTrack; NO: 关闭 AutoTrack
 */
- (BOOL)isAutoTrackEnabled;

#pragma mark - Ignore

/**
 * @abstract
 * 判断某个 AutoTrack 事件类型是否被忽略
 *
 * @param eventType SensorsAnalyticsAutoTrackEventType 要判断的 AutoTrack 事件类型
 *
 * @return YES:被忽略; NO:没有被忽略
 */
- (BOOL)isAutoTrackEventTypeIgnored:(SensorsAnalyticsAutoTrackEventType)eventType;

/**
 * @abstract
 * 忽略某一类型的 View
 *
 * @param aClass View 对应的 Class
 */
- (void)ignoreViewType:(Class)aClass;

/**
 * @abstract
 * 判断某个 View 类型是否被忽略
 *
 * @param aClass Class View 对应的 Class
 *
 * @return YES:被忽略; NO:没有被忽略
 */
- (BOOL)isViewTypeIgnored:(Class)aClass;

/**
 * @abstract
 * 在 AutoTrack 时，用户可以设置哪些 controllers 不被 AutoTrack
 *
 * @param controllers   controller ‘字符串’数组
 */
- (void)ignoreAutoTrackViewControllers:(NSArray<NSString *> *)controllers;

/**
 * @abstract
 * 判断某个 ViewController 是否被忽略
 *
 * @param viewController UIViewController
 *
 * @return YES:被忽略; NO:没有被忽略
 */
- (BOOL)isViewControllerIgnored:(UIViewController *)viewController;

#pragma mark - Track

/**
 * @abstract
 * 通过代码触发 UIView 的 $AppClick 事件
 *
 * @param view UIView
 */
- (void)trackViewAppClick:(nonnull UIView *)view;

/**
 * @abstract
 * 通过代码触发 UIView 的 $AppClick 事件
 *
 * @param view UIView
 * @param properties 自定义属性
 */
- (void)trackViewAppClick:(nonnull UIView *)view withProperties:(nullable NSDictionary *)properties;

/**
 * @abstract
 * 通过代码触发 UIViewController 的 $AppViewScreen 事件
 *
 * @param viewController 当前的 UIViewController
 */
- (void)trackViewScreen:(UIViewController *)viewController;
- (void)trackViewScreen:(UIViewController *)viewController properties:(nullable NSDictionary<NSString *,id> *)properties;

/**
 * @abstract
 * Track $AppViewScreen事件
 *
 * @param url 当前页面url
 * @param properties 用户扩展属性
 */
- (void)trackViewScreen:(NSString *)url withProperties:(NSDictionary *)properties;

#pragma mark - Deprecated

/**
 * @property
 *
 * @abstract
 * 打开 SDK 自动追踪,默认只追踪App 启动 / 关闭、进入页面、元素点击
 * @discussion
 * 该功能自动追踪 App 的一些行为，例如 SDK 初始化、App 启动 / 关闭、进入页面 等等，具体信息请参考文档:
 *   https://sensorsdata.cn/manual/ios_sdk.html
 * 该功能默认关闭
 */
- (void)enableAutoTrack:(SensorsAnalyticsAutoTrackEventType)eventType __attribute__((deprecated("已过时，请参考 SAConfigOptions 类的 autoTrackEventType")));

@end

@interface SAConfigOptions (AutoTrack)

///开启自动采集页面浏览时长
@property (nonatomic, assign) BOOL enableTrackPageLeave API_UNAVAILABLE(macos);


/// 是否开启子页面的页面浏览时长
@property (nonatomic, assign) BOOL enableTrackChildPageLeave API_UNAVAILABLE(macos);


/// 忽略特定页面的页面浏览时长采集
/// @param viewControllers 需要忽略的页面控制器的类
- (void)ignorePageLeave:(NSArray<Class>*)viewControllers;

/// 是否自动采集子页面的页面浏览事件
///
/// 开启页面浏览事件采集时，有效。默认为不采集
@property (nonatomic) BOOL enableAutoTrackChildViewScreen API_UNAVAILABLE(macos);

/**
 * @property
 *
 * @abstract
 * 打开 SDK 自动追踪,默认只追踪 App 启动 / 关闭、进入页面、元素点击
 *
 * @discussion
 * 该功能自动追踪 App 的一些行为，例如 SDK 初始化、App 启动 / 关闭、进入页面 等等，具体信息请参考文档:
 *   https://sensorsdata.cn/manual/ios_sdk.html
 * 该功能默认关闭
 */
@property (nonatomic) SensorsAnalyticsAutoTrackEventType autoTrackEventType API_UNAVAILABLE(macos);

@end

@interface SensorsAnalyticsSDK (SAReferrer)


/**
 * @abstract
 * 获取 LastScreenUrl
 *
 * @return LastScreenUrl
 */
- (NSString *)getLastScreenUrl API_UNAVAILABLE(macos);

/**
 * @abstract
 * 获取 LastScreenTrackProperties
 *
 * @return LastScreenTrackProperties
 */
- (NSDictionary *)getLastScreenTrackProperties API_UNAVAILABLE(macos);

/**
 * @abstract
 * App 退出或进到后台时清空 referrer，默认情况下不清空
 */
- (void)clearReferrerWhenAppEnd API_UNAVAILABLE(macos);

@end

NS_ASSUME_NONNULL_END
