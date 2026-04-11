// ***********************************************
// This example commands.js shows you how to
// create various custom commands and overwrite
// existing commands.
//
// For more comprehensive examples of custom
// commands please read more here:
// https://on.cypress.io/custom-commands
// ***********************************************
//
//
// -- This is a parent command --
// Cypress.Commands.add('login', (email, password) => { ... })
//
//
// -- This is a child command --
// Cypress.Commands.add('drag', { prevSubject: 'element'}, (subject, options) => { ... })
//
//
// -- This is a dual command --
// Cypress.Commands.add('dismiss', { prevSubject: 'optional'}, (subject, options) => { ... })
//
//
// -- This will overwrite an existing command --
// Cypress.Commands.overwrite('visit', (originalFn, url, options) => { ... })

export const main = '/';
export const addPerson = '/AddPerson';
export const documentation = '/Documentation';

Cypress.Commands.add('setNetwork', (options) => {
  // Use Chrome DevTools Protocol to emulate network conditions
  return Cypress.automation('remote:debugger:protocol', {
    command: 'Network.emulateNetworkConditions',
    params: {
      offline: false,
      latency: options.latency, // round‑trip latency in ms
      downloadThroughput: options.download, // bytes per second
      uploadThroughput: options.upload, // bytes per second
    },
  });
});

Cypress.Commands.add('resetNetwork', () => {
  // Disable throttling and restore normal network conditions
  return Cypress.automation('remote:debugger:protocol', {
    command: 'Network.emulateNetworkConditions',
    params: {
      offline: false,
      latency: -1,
      downloadThroughput: -1,
      uploadThroughput: -1,
    },
  }).then(() => {
    return Cypress.automation('remote:debugger:protocol', {
      command: 'Network.disable',
    });
  });
});
