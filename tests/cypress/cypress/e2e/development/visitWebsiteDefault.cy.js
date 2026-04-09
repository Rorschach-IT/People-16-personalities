// Automated test for checking response 200 OK for website
import { main, addPerson, documentation } from '../../support/commands';

/*
    This test suite uses Cypress to simulate different network conditions and verify that a React application loads correctly under those conditions. It includes two tests: one for slow 4G-like conditions and another for very poor 3G-like conditions. After each test, the network conditions are reset to ensure that subsequent tests are not affected. The tests check for the presence of a specific element (in this case, "My React App") to confirm that the app has rendered successfully.
*/

/*
    This approach works only in Chrome-based browsers due to the use of the Chrome DevTools Protocol for network emulation. If you need to test in other browsers, you may need to use a different method or tool for simulating network conditions, such as a proxy server or a third-party service that can throttle network speeds.
*/

describe('React app under different network conditions', () => {
  afterEach(() => {
    // Always reset network conditions after each test
    cy.resetNetwork();
  });

  const mainPage = 'Filtruj';
  const addPersonPage = 'Potwierdź';
  const documentationPage =
    '2026 - Lokalne typy osobowości - wczesny dostęp w wersji Alpha-0.1.1';

  it('Website default content loads correctly with slow connection (approx. Slow 4G)', () => {
    // Emulate Slow 4G‑like conditions
    // ~400ms latency, ~1 Mbps down, ~750 Kbps up
    cy.setNetwork({
      latency: 400, // 400 ms RTT
      download: 125000, // 1 Mbps in bytes/sec
      upload: 93750, // 750 Kbps in bytes/sec
    });

    cy.visit(main);
    cy.contains(mainPage).should('be.visible');

    cy.visit(addPerson);
    cy.contains(addPersonPage).should('be.visible');

    cy.visit(documentation);
    cy.contains(documentationPage).should('be.visible');
  });

  it('Website default contentloads correctly with very poor connection (approx. Regular 3G)', () => {
    // Emulate very poor 3G‑like conditions
    // ~1000ms latency, ~750 Kbps down, ~250 Kbps up
    cy.setNetwork({
      latency: 1000, // 1 second RTT
      download: 93750, // 750 Kbps in bytes/sec
      upload: 31250, // 250 Kbps in bytes/sec
    });

    cy.visit(main);
    cy.contains(mainPage).should('be.visible');
    cy.contains('Jakub').should('be.visible');
    cy.contains('Siwek').should('be.visible');
    cy.contains('Maison').should('be.visible');
    cy.contains('Zarzycka').should('be.visible');

    cy.visit(addPerson);
    cy.contains(addPersonPage).should('be.visible');
    cy.contains('Imię').should('be.visible');
    cy.contains('ESFJ').should('be.visible');

    cy.visit(documentation);
    cy.contains(documentationPage).should('be.visible');
  });
});
