const { defineConfig } = require('cypress');

module.exports = defineConfig({
  allowCypressEnv: false,

  e2e: {
    setupNodeEvents(on, config) {},
    baseUrl: 'http://localhost:5188/',
    specPattern: 'cypress/e2e/**/*.cy.{js,ts}',
    supportFile: 'cypress/support/e2e.js',
    viewportWidth: 1920,
    viewportHeight: 1080,
    video: false,
    chromeWebSecurity: false,
    redirectionLimit: 3,
    watchForFileChanges: true,
    testIsolation: false,
    waitForAnimations: true,
    retries: {
      runMode: 1,
      openMode: 1,
    },
  },
});
