<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
	<xsl:output method="html" indent="yes"/>
	<xsl:template match="/">
		<html>
			<body>
				<p>
					Hi <xsl:value-of select="TokenModel/Name"/>,
				</p>
				<p>
					<span>Click </span>
					<xsl:element name="a">
						<xsl:attribute name="href">
							<xsl:value-of select="TokenModel/Url"/>
						</xsl:attribute>
						<xsl:value-of select="TokenModel/Text"/>
					</xsl:element>
					<span> to reset password.</span>
				</p>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>
