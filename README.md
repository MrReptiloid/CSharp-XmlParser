Exemple of using:

    var xmlReader = new FileXmlReader();
    var headerParser = new XmlHeaderParser();
    var attributeExtractor = new AttributeExtractor();
    var elementParser = new XmlElementParser(attributeExtractor);
    
    var xmlParser = new XmlParser(xmlReader, headerParser, elementParser);
    XmlDocument xmlDocument = xmlParser.Parse("../../../Data/test.xml");

For search:

    var rootElement = xmlDocument.RootElement;
    
    var tagSearch = new TagNameExpression("book");
    var attributeSearch = new AttributeExpression("id", "1");
    var textSearch = new TextExpression("1949");
    var combinedSearch = new AndExpression(tagSearch, attributeSearch);
    
    var resultByTag = tagSearch.Interpret(rootElement);
    foreach (var item in resultByTag)
        XmlElement.Print(item, 0);
    
    var resultByAttribute = attributeSearch.Interpret(rootElement);
    foreach (var item in resultByAttribute)
        XmlElement.Print(item, 0);
    
    var resultByText = textSearch.Interpret(rootElement);
    foreach (var item in resultByText)
        XmlElement.Print(item, 0);
    
    var resultCombined = combinedSearch.Interpret(rootElement);
    foreach (var item in resultCombined)
        XmlElement.Print(item, 0);
