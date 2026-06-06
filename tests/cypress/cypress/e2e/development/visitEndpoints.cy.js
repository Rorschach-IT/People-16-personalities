/*
    This approach works only in Chrome-based browsers due to the use of the Chrome DevTools Protocol for network emulation. If you need to test in other browsers, you may need to use a different method or tool for simulating network conditions, such as a proxy server or a third-party service that can throttle network speeds.
*/

describe('Check if HTTP response for url is true', () => {
  it('Opening endpoint INTJ', () => {
    cy.visit('/?type=INTJ');
    cy.contains('Sosnowska').should('be.visible');
  });

  it('Opening endpoint INTP', () => {
    cy.visit('/?type=INTP');
    cy.contains('Fraś').should('be.visible');
  });

  it('Opening endpoint ENTJ', () => {
    cy.visit('/?type=ENTJ');
    cy.contains('Jastrzębska').should('be.visible');
  });

  it('Opening endpoint ENTP', () => {
    cy.visit('/?type=ENTP');
    cy.contains('Lemański').should('be.visible');
  });

  it('Opening endpoint INFJ', () => {
    cy.visit('/?type=INFJ');
    cy.contains('Kulesza').should('be.visible');
  });

  it('Opening endpoint INFP', () => {
    cy.visit('/?type=INFP');
    cy.contains('Nowosad').should('be.visible');
  });

  it('Opening endpoint ENFJ', () => {
    cy.visit('/?type=ENFJ');
    cy.contains('Wrona').should('be.visible');
  });

  it('Opening endpoint ENFP', () => {
    cy.visit('/?type=ENFP');
    cy.contains('Tokarska').should('be.visible');
  });

  it('Opening endpoint ISTJ', () => {
    cy.visit('/?type=ISTJ');
    cy.contains('Lisowska').should('be.visible');
  });

  it('Opening endpoint ISFJ', () => {
    cy.visit('/?type=ISFJ');
    cy.contains('Bednarek').should('be.visible');
  });

  it('Opening endpoint ESTJ', () => {
    cy.visit('/?type=ESTJ');
    cy.contains('Borowiec').should('be.visible');
  });

  it('Opening endpoint ESFJ', () => {
    cy.visit('/?type=ESFJ');
    cy.contains('Cieślar').should('be.visible');
  });

  it('Opening endpoint ISTP', () => {
    cy.visit('/?type=ISTP');
    cy.contains('Wesołowska').should('be.visible');
  });

  it('Opening endpoint ISFP', () => {
    cy.visit('/?type=ISFP');
    cy.contains('Olchowska').should('be.visible');
  });

  it('Opening endpoint ESTP', () => {
    cy.visit('/?type=ESTP');
    cy.contains('Kalinowski').should('be.visible');
  });

  it('Opening endpoint ESFP', () => {
    cy.visit('/?type=ESFP');
    cy.contains('Kozera').should('be.visible');
  });
});
