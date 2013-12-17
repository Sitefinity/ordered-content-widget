// Karma configuration
// Generated on Mon Dec 16 2013 00:00:45 GMT+0200 (FLE Standard Time)

module.exports = function(config) {
  config.set({

    // base path, that will be used to resolve files and exclude
    basePath: '',


    // frameworks to use
    frameworks: ['jasmine'],


    // list of files / patterns to load in the browser
    files: [
      'lib/angular.min.js',
      'lib/angular-resource.min.js',
      'lib/angular-mocks.js',
      'lib/jquery.min.js',
      'lib/jquery-ui.min.js',
      'lib/angular-ui.min.js',
      'lib/sortable.js',
      'lib/MicrosoftAjax.js',
      'lib/IControlDesigner.js',
      'lib/ControlDesignerBase.js',
      '../Telerik.Sitefinity.OrderedContentWidget/Scripts/*.js',
      '../Telerik.Sitefinity.OrderedContentWidget/Scripts/lib/*.js',
      'DesignerSpec.js'
    ],


    // list of files to exclude
    exclude: [
      
    ],


    // test results reporter to use
    // possible values: 'dots', 'progress', 'junit', 'growl', 'coverage'
    reporters: ['progress'],


    // web server port
    port: 9876,


    // enable / disable colors in the output (reporters and logs)
    colors: true,


    // level of logging
    // possible values: config.LOG_DISABLE || config.LOG_ERROR || config.LOG_WARN || config.LOG_INFO || config.LOG_DEBUG
    logLevel: config.LOG_INFO,


    // enable / disable watching file and executing tests whenever any file changes
    autoWatch: true,


    // Start these browsers, currently available:
    // - Chrome
    // - ChromeCanary
    // - Firefox
    // - Opera
    // - Safari (only Mac)
    // - PhantomJS
    // - IE (only Windows)
    browsers: ['Firefox'],


    // If browser does not capture in given timeout [ms], kill it
    captureTimeout: 60000,


    // Continuous Integration mode
    // if true, it capture browsers, run tests and exit
    singleRun: false
  });
};
