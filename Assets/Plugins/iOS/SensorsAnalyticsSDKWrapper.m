#import <SensorsAnalyticsSDK/SensorsAnalyticsSDK.h>

void convertToDictionary(const char *json, NSDictionary **properties_dict) {
    NSString *json_string = json != NULL ? [NSString stringWithUTF8String:json] : nil;
    if (json_string) {
        *properties_dict = [NSJSONSerialization JSONObjectWithData:[json_string dataUsingEncoding:NSUTF8StringEncoding] options:kNilOptions error:nil];
    }
}

void start(const char *url, bool enable_log, int eventType, int network_type) {
    NSString *serverURL = [NSString stringWithCString:url encoding:NSUTF8StringEncoding];
    SAConfigOptions *options = [[SAConfigOptions alloc] initWithServerURL:serverURL launchOptions:nil];
    options.autoTrackEventType = eventType;
    options.enableLog = enable_log;
    [SensorsAnalyticsSDK startWithConfigOptions:options];
    [[SensorsAnalyticsSDK sharedInstance] setFlushNetworkPolicy:network_type];
}

void identify(const char *anonymous_id) {
    NSString *anonymousId = [NSString stringWithCString:anonymous_id encoding:NSUTF8StringEncoding];
    [[SensorsAnalyticsSDK sharedInstance] identify:anonymousId];
}

void track(const char *eventName, const char *properties) {
    NSString *event = [NSString stringWithCString:eventName encoding:NSUTF8StringEncoding];
    NSDictionary *eventProps = nil;
    convertToDictionary(properties, &eventProps);
    [[SensorsAnalyticsSDK sharedInstance] track:event withProperties:eventProps];
}

void login(const char *loginId) {
    NSString *newLoginId = [NSString stringWithCString:loginId encoding:NSUTF8StringEncoding];
    [[SensorsAnalyticsSDK sharedInstance] login:newLoginId];
}

void logout() {
    [[SensorsAnalyticsSDK sharedInstance] logout];
}

void profile_set(const char *properties) {
    NSDictionary *profileProps = nil;
    convertToDictionary(properties, &profileProps);
    [[SensorsAnalyticsSDK sharedInstance] set:profileProps];
}

char* get_super_properties() {
    NSDictionary *properties = [[SensorsAnalyticsSDK sharedInstance] currentSuperProperties];
    NSData *jsonData = [NSJSONSerialization dataWithJSONObject:properties options:NSJSONWritingPrettyPrinted error:nil];
    NSString *jsonStr = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
    return strdup([jsonStr UTF8String]);;
}

void flush() {
    [[SensorsAnalyticsSDK sharedInstance] flush];
}

char* track_timer_start(const char *eventName) {
    NSString *newEventName = [NSString stringWithCString:eventName encoding:NSUTF8StringEncoding];
    NSString *timerId = [[SensorsAnalyticsSDK sharedInstance] trackTimerStart:newEventName];
    return strdup([timerId UTF8String]);;
}

void track_timer_pause(const char *eventName) {
    NSString *newEventName = [NSString stringWithCString:eventName encoding:NSUTF8StringEncoding];
    [[SensorsAnalyticsSDK sharedInstance] trackTimerPause:newEventName];
}

void track_timer_resume(const char *eventName) {
    NSString *newEventName = [NSString stringWithCString:eventName encoding:NSUTF8StringEncoding];
    [[SensorsAnalyticsSDK sharedInstance] trackTimerResume:newEventName];
}

void track_timer_end(const char *eventName, const char *properties) {
    NSString *newEventName = [NSString stringWithCString:eventName encoding:NSUTF8StringEncoding];
    NSDictionary *eventProps = nil;
    convertToDictionary(properties, &eventProps);
    [[SensorsAnalyticsSDK sharedInstance] trackTimerEnd:newEventName withProperties:eventProps];
}

void clear_track_timer() {
    [[SensorsAnalyticsSDK sharedInstance] clearTrackTimer];
}

void register_super_properties(const char *properties) {
    NSDictionary *eventProps = nil;
    convertToDictionary(properties, &eventProps);
    [[SensorsAnalyticsSDK sharedInstance] registerSuperProperties:eventProps];
}

void unregister_super_property(const char *superPropertyName) {
    NSString *propertyName = [NSString stringWithCString:superPropertyName encoding:NSUTF8StringEncoding];
    [[SensorsAnalyticsSDK sharedInstance] unregisterSuperProperty:propertyName];
}

void clear_super_properties() {
    [[SensorsAnalyticsSDK sharedInstance] clearSuperProperties];
}

#pragma mark - 二期接口
void delete_all() {
    [[SensorsAnalyticsSDK sharedInstance] deleteAll];
}

void set_flush_network_policy(int types) {
    [[SensorsAnalyticsSDK sharedInstance] setFlushNetworkPolicy:types];
}

void profile_set_once(const char *properties) {
    NSDictionary *profileProps = nil;
    convertToDictionary(properties, &profileProps);
    [[SensorsAnalyticsSDK sharedInstance] setOnce:profileProps];
}

void remove_timer(const char *eventName) {
    NSString *newEventName = [NSString stringWithCString:eventName encoding:NSUTF8StringEncoding];
    [[SensorsAnalyticsSDK sharedInstance] removeTimer:newEventName];
}

void track_app_install(const char *properties, bool disableCallback) {
    NSDictionary *props = nil;
    convertToDictionary(properties, &props);
    [[SensorsAnalyticsSDK sharedInstance] trackAppInstallWithProperties:props disableCallback:disableCallback];
}

#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Wdeprecated-declarations"
void set_ios_max_cache_size(int count) {
    [[SensorsAnalyticsSDK sharedInstance] setMaxCacheSize:count];
}

void set_flush_bulk_size(int flushBulkSize) {
    [[SensorsAnalyticsSDK sharedInstance] setFlushBulkSize:flushBulkSize];
}

void set_flush_interval(int flushInteval) {
    [[SensorsAnalyticsSDK sharedInstance] setFlushInterval:flushInteval];
}
#pragma clang diagnostic pop