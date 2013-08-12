<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
	<xsl:output method="html" indent="yes"/>
	<xsl:template match="/">
		<html>
			<body>
				<span>Click </span>
				<xsl:element name="a">
					<xsl:attribute name="href">
						<xsl:value-of select="TokenModel/Url"/>
					</xsl:attribute>
					<xsl:value-of select="TokenModel/Text"/>
				</xsl:element>
				<span> to proceed to account activation.</span>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>
