import { CompanyWebsiteTemplatePage } from './app.po';

describe('CompanyWebsite App', function() {
  let page: CompanyWebsiteTemplatePage;

  beforeEach(() => {
    page = new CompanyWebsiteTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
