require 'albacore'

SOLUTION_DIR = "winter.auth.service.sln"
PACKAGES_DIR = "packages"
NUGET_DIR = "c:/program files (x86)/nuget/nuget.exe"
WEB_PROJECT_BIN_DIR = "bin/"
INTEGRATION_TEST_PROJECT = "winter.auth.service.test.integration"

task :default => [:build]

desc 'restore all nugets as per the packages.config files'
nugets_restore :restore do |p|
  p.out = PACKAGES_DIR
  p.exe = NUGET_DIR
end

desc "Buildling"
build :build => [:restore] do |b|
  b.sln = SOLUTION_DIR
  b.target = ['Clean', 'Rebuild']
  b.prop 'Configuration', 'Release'
end


desc "Publishing"
build :publish => [:restore] do |b|
  b.sln = SOLUTION_DIR
  b.target = ['Clean', 'Rebuild']
  b.prop 'Configuration', 'Release'
  b.prop 'UseWPP_CopyWebApplication', 'true'    # applies the web.config transforms for the build config
  b.prop 'PipelineDependsOnBuild', 'false'      # makes it so you can package without building first
  b.prop 'webprojectoutputdir', '../output/'       # the directory to write the published files to
  b.prop 'outdir', WEB_PROJECT_BIN_DIR          # the directory of your bin files for the project
end

desc "Transforming Test Config"
task :transformTestConfig, :environment do |t, args|
    FileUtils.cp "#{INTEGRATION_TEST_PROJECT}/config/app.#{args.environment}.config", "#{INTEGRATION_TEST_PROJECT}/bin/#{INTEGRATION_TEST_PROJECT}.dll.config"
end